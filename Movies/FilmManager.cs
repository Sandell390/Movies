
using System.Collections.Generic;
using System.Linq;

namespace Movies
{
    public static class FilmManager
    {
        //Returner alle film
        public static List<Film> GetFilm()
        {
            return DalManager.GetFilms();
        }

        //Returner alle skuespiller
        public static List<Actor> GetActor() 
        {
            return DalManager.GetActors();
        }
        
        public static List<Genre> GetGenre() 
        {
            return DalManager.GetGenre();
        }

        //Returner alle genre navne
        public static List<Genre> GetGenreName() 
        {
            return DalManager.GetGenre();
        }

        //Returner alle skuespiller i en bestemt film
        public static List<Actor> GetActorInMovies(int movie_id, List<Film> movies) 
        {
            List<Actor> actorsName = new();

            List<ActorInMovie> actorInMovies = DalManager.GetActorInMovies();

            List<Actor> actors = GetActor();


            
            //Tjekker om movie_id på actor er det samme id på den valgte film
            for (int i = 0; i < actorInMovies.Count; i++)
            {
                if (actorInMovies[i].Movie_id == movie_id)
                {
                    for (int j = 0; j < actors.Count; j++)
                    {
                        if (actors[j].Id == actorInMovies[i].Actor_id) 
                        {
                            actorsName.Add(actors[j]);
                        }
                    }
                }
            }
            return actorsName;
        }

        //Returner alle film som i en bestemt skuespiller har været med i
        public static List<Film> GetFilmsFromActor(int actor_id, List<Actor> actors) 
        {
            List<Film> MovieName = new();

            List<ActorInMovie> actorInMovies = DalManager.GetActorInMovies();

            List<Film> films = GetFilm();

            //Tjekker om actor_id på film er det samme id på den valgte skuespiller
            foreach (ActorInMovie film in actorInMovies)
            {
                if (film.Actor_id == actor_id) 
                {
                    MovieName.Add(films.Find(x => x.Id == film.Movie_id));
                }
            }
            return MovieName;

        }

        //Returner alle film fra en search sting
        public static List<Film> GetSearchFilm(string searchName) 
        {
            return DalManager.GetFilmsFromSearch(searchName);
        
        }

        //Returner alle skuspiller fra en search sting
        public static List<Actor> GetSearchActor(string SearchName) 
        {
            return DalManager.GetActorsFromSearch(SearchName);
        }

        //Returner alle film på en bestemt genre 
        public static List<Film> GetMoviesFromGenre(Genre genre) 
        {
            List<Film> films = GetFilm();
            List<Film> selectedFilms = new();

            foreach (Film film in films)
            {
                foreach (Genre genre1 in film.Genre)
                {
                    if (genre1.Id == genre.Id) 
                    {
                        selectedFilms.Add(film);
                    }
                }
            
            }
            return selectedFilms;
        }

        public static Actor InsertActor (Actor a) 
        {
            return DalManager.InsertActor(a);
        }

        public static Film InsertFilm(Film f, int genre) 
        {
            return DalManager.InsertFilm(f, genre);
        }

        public static Genre InsertGenre(Genre g) 
        {
            return DalManager.InsertGenre(g);
        }

        public static Film UpdateFilmData(Film f) 
        {
            return DalManager.UpdateFilmData(f);
        }

        public static void InsertActorInMovie(ActorInMovie a) 
        {
            DalManager.InsertActorInMovie(a);
        }

        public static void DeleteActorInMovie(ActorInMovie a)
        {
            DalManager.DeleteActorInMovie(a);
        }

        public static void InsertGenreMovie(int movie_id, int genre_id) 
        {
            DalManager.InsertGenreMovie(movie_id, genre_id);
        }

        public static void DeleteGenreFromMovie(Genre g, int movie_id)
        {
            DalManager.DeleteGenreFromMovie(g,movie_id);
        }

        public static Actor UpdateActorData(Actor a) 
        {
            return DalManager.UpdateActorData(a);
        }

        public static void DeleteMovie(int movie_id) 
        {
            DalManager.DeleteMovie(movie_id);
        }
        public static void DeleteActor(int actor_id)
        {
            DalManager.DeleteActor(actor_id);
        }

        public static void DeleteGenre(int genre_id)
        {
            DalManager.DeleteGenre(genre_id);
        }
    }
}
