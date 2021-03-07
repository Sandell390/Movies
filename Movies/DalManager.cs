
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace Movies
{
    //Håndter forspørgelser for database og connection til database
    public static class DalManager
    {
        private static string cs = @"Data Source=PROGAMER\SQL2019;Initial Catalog=MCU; Integrated Security=SSPI";

        //Får alle film fra database
        public static List<Film> GetFilms()
        {
            List<Film> films = new();

            using (SqlConnection connection = new(cs))
            {
                connection.Open();

                SqlCommand cmd = new("select movie.id as [movie_id], movie.title,movie.year, genre.name, genre.id as [genre_id] from genre_movie " +
                    "left join movie  " +
                    "on movie.id = genre_movie.movie_id " +
                    "left join genre " +
                    "on genre.id = genre_movie.genre_id ", connection);

                SqlDataReader rdr = cmd.ExecuteReader();


                
                while (rdr.Read())
                {
                    

                    //henter data fra readeren og "caster"
                    //til den rigtige datatype
                    int id = (int)rdr["movie_id"];
                    string title = (string)rdr["title"];
                    int year = (int)rdr["year"];

                    if (films.Where(p => p.Title == title).Count() > 0)
                    {
                        films.Find(p => p.Title == title).Genre.Add(new Genre((int)rdr["genre_id"],(string)rdr["name"]));
                    }
                    else
                    {
                        List<Genre> genreList = new List<Genre>();
                        genreList.Add(new Genre((int)rdr["genre_id"], (string)rdr["name"]));


                        //Opretter et ny film objekt
                        Film f = new(id, title, year, genreList);
                        //tilføjer film til listen
                        films.Add(f);
                    }



                    
                }

            }

            return films;
        }

        //Får alle skuespiller fra database
        public static List<Actor> GetActors()
        {
            List<Actor> actors = new();

            using (SqlConnection connection = new(cs))
            {
                connection.Open();

                SqlCommand cmd = new("select id, firstname, lastname from actor", connection);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //henter data fra readeren og "caster"
                    //til den rigtige datatype
                    int id = (int)rdr["id"];
                    string firstname = (string)rdr["firstname"];
                    string lastname = (string)rdr["lastname"];
                    //Opretter et ny actor objekt
                    Actor a = new(id, firstname,lastname);
                    //tilføjer actor til listen
                    actors.Add(a);
                }

            }

            return actors;
        }

        //Får data fra mange-mange tablet mellem Film og skuespiller
        public static List<ActorInMovie> GetActorInMovies()
        {
            List<ActorInMovie> actorInMovies = new();

            using (SqlConnection connection = new(cs))
            {
                connection.Open();

                SqlCommand cmd = new("select movie_id, actor_id from movie_actor", connection);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //henter data fra readeren og "caster"
                    //til den rigtige datatype
                    int movie_id = (int)rdr["movie_id"];
                    int actor_id = (int)rdr["actor_id"];

                    //Opretter et ny ActorInMovie objekt
                    ActorInMovie a = new(movie_id,actor_id);
                    //tilføjer ActorInMovie til listen
                    actorInMovies.Add(a);
                }

            }

            return actorInMovies;
        }

        //Får alle film ud fra search sting
        public static List<Film> GetFilmsFromSearch(string searchMessage)
        {
            List<Film> films = new();

            using (SqlConnection connection = new(cs))
            {
                connection.Open();

                SqlCommand cmd = new("select movie.id as [movie_id], movie.title,movie.year, genre.name, genre.id as [genre_id] from genre_movie " +
                    "left join movie " +
                    "on movie.id = genre_movie.movie_id "  +
                    "left join genre " +
                    "on genre.id = genre_movie.genre_id " +
                    "where movie.title like @search", connection);


                SqlParameter sp = new();

                sp.ParameterName = "@search";

                sp.Value = "%" + searchMessage + "%";

                cmd.Parameters.Add(sp);


                SqlDataReader rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    //henter data fra readeren og "caster"
                    //til den rigtige datatype
                    int id = (int)rdr["movie_id"];
                    string title = (string)rdr["title"];
                    int year = (int)rdr["year"];

                    if (films.Where(p => p.Title == title).Count() > 0)
                    {
                        films.Find(p => p.Title == title).Genre.Add(new Genre((int)rdr["genre_id"], (string)rdr["name"]));
                    }
                    else
                    {
                        List<Genre> genreList = new List<Genre>();
                        genreList.Add(new Genre((int)rdr["genre_id"], (string)rdr["name"]));


                        //Opretter et ny film objekt
                        Film f = new(id, title, year, genreList);
                        //tilføjer film til listen
                        films.Add(f);
                    }
                }

            }

            return films;
        }

        //Får alle skuespiller ud fra search sting
        public static List<Actor> GetActorsFromSearch(string searchMessage)
        {
            List<Actor> actors = new();

            using (SqlConnection connection = new(cs))
            {
                connection.Open();

                SqlCommand cmd = new("select id, firstname, lastname from actor where firstname like @search", connection);


                SqlParameter sp = new();

                sp.ParameterName = "@search";

                sp.Value = "%" + searchMessage + "%";

                cmd.Parameters.Add(sp);


                SqlDataReader rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    //henter data fra readeren og "caster"
                    //til den rigtige datatype
                    int id = (int)rdr["id"];
                    string firstname = (string)rdr["firstname"];
                    string lastname = (string)rdr["lastname"];
                    //Opretter et actor film objekt
                    Actor a = new(id, firstname, lastname);
                    //tilføjer actor til listen
                    actors.Add(a);
                }

            }

            return actors;
        }

        //Får alle genre fra database
        public static List<Genre> GetGenre() 
        {
            List<Genre> genre = new();

            using (SqlConnection connection = new(cs))
            {
                connection.Open();

                SqlCommand cmd = new("select id,name from genre", connection);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //henter data fra readeren og "caster"
                    //til den rigtige datatype
                    int id = (int)rdr["id"];
                    string name = (string)rdr["name"];

                    Genre g = new(id, name);

                    //tilføjer gerne til listen
                    genre.Add(g);
                }

            }

            return genre;

        }

        //Indsætter Actor
        public static Actor InsertActor(Actor a)
        {
            using (SqlConnection connection = new(cs))
            {
                //Opretter en forbindelse til databasen
                connection.Open();
                //insert data
                SqlCommand cmd = new SqlCommand
                ("insert into actor(firstname,lastname) OUTPUT INSERTED.ID values(@fn,@ln)",
                connection);
                //tilføjer parametre
                cmd.Parameters.Add(new SqlParameter("@fn", a.Firstname));
                cmd.Parameters.Add(new SqlParameter("@ln", a.Lastname));
                
                int newId = (int)cmd.ExecuteScalar();
                //Id sættes ind i a
                a.Id = newId;
            }
            return a;
        }

        public static Film InsertFilm(Film f, int genre)
        {
            using (SqlConnection connection = new(cs))
            {
                //Opretter en forbindelse til databasen
                connection.Open();
                //insert data
                SqlCommand cmd = new SqlCommand
                ("insert into movie(title,year) OUTPUT INSERTED.ID values(@title,@y)",
                connection);
                //tilføjer parametre
                cmd.Parameters.Add(new SqlParameter("@title", f.Title));
                cmd.Parameters.Add(new SqlParameter("@y", f.Year));

                int newId = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand
                ("insert into genre_movie(movie_id,genre_id) values (@m_id,@g_id)",
                connection);

                cmd.Parameters.Add(new SqlParameter("@m_id", newId));
                cmd.Parameters.Add(new SqlParameter("@g_id", genre));

                cmd.ExecuteNonQuery();

                //Id sættes ind i a
                f.Id = newId;
            }
            return f;
        }

        public static Genre InsertGenre(Genre g) 
        {
            using (SqlConnection connection = new(cs))
            {
                //Opretter en forbindelse til databasen
                connection.Open();
                //insert data
                SqlCommand cmd = new SqlCommand
                ("insert into genre(name) OUTPUT INSERTED.ID values(@name)",
                connection);
                //tilføjer parametre
                cmd.Parameters.Add(new SqlParameter("@name", g.Name));

                int newId = (int)cmd.ExecuteScalar();

                //Id sættes ind i a
                g.Id = newId;
            }
            return g;
        }

        public static Film UpdateFilmData(Film f) 
        {
            using (SqlConnection connection = new(cs))
            {
                //Opretter en forbindelse til databasen
                connection.Open();
                //update data
                SqlCommand cmd = new SqlCommand
                ("update movie set title = @title, year = @year where id = @id",
                connection);
                //tilføjer parametre
                cmd.Parameters.Add(new SqlParameter("@title", f.Title));
                cmd.Parameters.Add(new SqlParameter("@year", f.Year));
                cmd.Parameters.Add(new SqlParameter("@id", f.Id));

                cmd.ExecuteNonQuery();

                //Id sættes ind i a
                
            }
            return f;
        }

        public static Actor UpdateActorData(Actor a) 
        {
            using (SqlConnection connection = new(cs))
            {
                //Opretter en forbindelse til databasen
                connection.Open();
                //update data
                SqlCommand cmd = new SqlCommand
                ("update actor set firstname = @fn, lastname = @ln where id = @id",
                connection);
                //tilføjer parametre
                cmd.Parameters.Add(new SqlParameter("@fn", a.Firstname));
                cmd.Parameters.Add(new SqlParameter("@ln", a.Lastname));
                cmd.Parameters.Add(new SqlParameter("@id", a.Id));

                cmd.ExecuteNonQuery();

                //Id sættes ind i a

            }
            return a;
        }

        public static void InsertActorInMovie(ActorInMovie a)
        {
            using (SqlConnection connection = new(cs))
            {
                //Opretter en forbindelse til databasen
                connection.Open();
                //update data
                SqlCommand cmd = new SqlCommand
                ("insert into movie_actor(movie_id, actor_id) values (@m_id, @a_id)",
                connection);
                //tilføjer parametre
                cmd.Parameters.Add(new SqlParameter("@m_id", a.Movie_id));
                cmd.Parameters.Add(new SqlParameter("@a_id", a.Actor_id));

                cmd.ExecuteNonQuery();
    
                //Id sættes ind i a

            }
        }
        public static void DeleteActorInMovie(ActorInMovie a)
        {
            using (SqlConnection connection = new(cs))
            {
                //Opretter en forbindelse til databasen
                connection.Open();
                //update data
                SqlCommand cmd = new SqlCommand
                ("Delete from movie_actor where movie_id = @m_id and actor_id = @a_id",
                connection);
                //tilføjer parametre
                cmd.Parameters.Add(new SqlParameter("@m_id", a.Movie_id));
                cmd.Parameters.Add(new SqlParameter("@a_id", a.Actor_id));

                cmd.ExecuteNonQuery();

                //Id sættes ind i a

            }
        }

        public static void InsertGenreMovie(int movie_id, int genre_id) 
        {
            using (SqlConnection connection = new(cs))
            {
                //Opretter en forbindelse til databasen
                connection.Open();
                //update data
                SqlCommand cmd = new SqlCommand
                ("insert into genre_movie(movie_id, genre_id) values (@m_id, @g_id)",
                connection);
                //tilføjer parametre
                cmd.Parameters.Add(new SqlParameter("@m_id", movie_id));
                cmd.Parameters.Add(new SqlParameter("@g_id", genre_id));

                cmd.ExecuteNonQuery();

                //Id sættes ind i a

            }
        }

        public static void DeleteGenreFromMovie(Genre g, int movie_id)
        {
            using (SqlConnection connection = new(cs))
            {
                //Opretter en forbindelse til databasen
                connection.Open();
                //update data
                SqlCommand cmd = new SqlCommand
                ("Delete from genre_movie where movie_id = @m_id and genre_id = @g_id",
                connection);
                //tilføjer parametre
                cmd.Parameters.Add(new SqlParameter("@m_id", movie_id));
                cmd.Parameters.Add(new SqlParameter("@g_id", g.Id));

                cmd.ExecuteNonQuery();

                //Id sættes ind i a

            }
        }

        public static void DeleteMovie(int movie_id) 
        {
            using (SqlConnection connection = new(cs))
            {
                //Opretter en forbindelse til databasen
                connection.Open();
                //update data
                SqlCommand cmd = new SqlCommand
                ("Delete from movie where id = @m_id",
                connection);
                //tilføjer parametre
                cmd.Parameters.Add(new SqlParameter("@m_id", movie_id));

                cmd.ExecuteNonQuery();

                //Id sættes ind i a

            }
        }

        public static void DeleteActor(int actor_id)
        {
            using (SqlConnection connection = new(cs))
            {
                //Opretter en forbindelse til databasen
                connection.Open();
                //update data
                SqlCommand cmd = new SqlCommand
                ("Delete from actor where id = @a_id",
                connection);
                //tilføjer parametre
                cmd.Parameters.Add(new SqlParameter("@a_id", actor_id));

                cmd.ExecuteNonQuery();

                //Id sættes ind i a

            }
        }

        public static void DeleteGenre(int genre_id)
        {
            using (SqlConnection connection = new(cs))
            {
                //Opretter en forbindelse til databasen
                connection.Open();
                //update data
                SqlCommand cmd = new SqlCommand
                ("Delete from genre where id = @g_id",
                connection);
                //tilføjer parametre
                cmd.Parameters.Add(new SqlParameter("@g_id", genre_id));

                cmd.ExecuteNonQuery();

                //Id sættes ind i a

            }
        }

    }
}
