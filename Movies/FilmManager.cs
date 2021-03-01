
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
        
        //Returner alle genre
        public static List<string> GetGenre() 
        {
            return DalManager.GetGenre();
        }

        //Returner alle skuespiller i en bestemt film
        public static List<string> GetActorInMovies(int movie_id, List<Film> movies) 
        {
            List<string> actorsName = new();

            List<ActorInMovie> actorInMovies = DalManager.GetActorInMovies();

            List<Actor> actors = GetActor();

            //Tjekker om movie_id på actor er det samme id på den valgte film
            foreach (ActorInMovie actor in actorInMovies)
            {
                if (actor.Movie_id == movies[movie_id].Id) 
                {
                    actorsName.Add(actors[actor.Actor_id].Firstname + " " + actors[actor.Actor_id].Lastname);
                }
            }
            return actorsName;
        }

        //Returner alle film som i en bestemt skuespiller har været med i
        public static List<string> GetFilmsFromActor(int actor_id, List<Actor> actors) 
        {
            List<string> MovieName = new();

            List<ActorInMovie> actorInMovies = DalManager.GetActorInMovies();

            List<Film> films = GetFilm();

            //Tjekker om actor_id på film er det samme id på den valgte skuespiller
            foreach (ActorInMovie film in actorInMovies)
            {
                if (film.Actor_id == actors[actor_id].Id) 
                {
                    MovieName.Add(films[film.Movie_id].Title);
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
        public static List<Film> GetMoviesFromGenre(string genre) 
        {
            List<Film> films = GetFilm().Where(x => x.Genre == genre).ToList();
            return films;
        }
    }
}
