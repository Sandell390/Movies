
using System.Collections.Generic;

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

                SqlCommand cmd = new("select id, title,year, genre from movie", connection);

                SqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    //henter data fra readeren og "caster"
                    //til den rigtige datatype
                    int id = (int)rdr["id"] - 1;
                    string title = (string)rdr["title"];
                    int year = (int)rdr["year"];
                    string genre = (string)rdr["genre"];
                    //Opretter et ny film objekt
                    Film f = new(id, title, year,genre);
                    //tilføjer film til listen
                    films.Add(f);
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
                    int id = (int)rdr["id"] - 1;
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
                    int movie_id = (int)rdr["movie_id"] - 1;
                    int actor_id = (int)rdr["actor_id"] - 1;

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

                SqlCommand cmd = new("select id, title,year, genre from movie where title like @search", connection);


                SqlParameter sp = new();

                sp.ParameterName = "@search";

                sp.Value = "%" + searchMessage + "%";

                cmd.Parameters.Add(sp);


                SqlDataReader rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    //henter data fra readeren og "caster"
                    //til den rigtige datatype
                    int id = (int)rdr["id"] - 1;
                    string title = (string)rdr["title"];
                    int year = (int)rdr["year"];
                    string genre = (string)rdr["genre"];
                    //Opretter et ny film objekt
                    Film f = new(id, title, year, genre);
                    //tilføjer film til listen
                    films.Add(f);
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
                    int id = (int)rdr["id"] - 1;
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
        public static List<string> GetGenre() 
        {
            List<string> genre = new();

            using (SqlConnection connection = new(cs))
            {
                connection.Open();

                SqlCommand cmd = new("select genre from movie group by genre", connection);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //henter data fra readeren og "caster"
                    //til den rigtige datatype
                    string name = (string)rdr["genre"];

                    //tilføjer gerne til listen
                    genre.Add(name);
                }

            }

            return genre;

        }

    }
}
