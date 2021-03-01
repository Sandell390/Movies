using System;
using System.Collections.Generic;

namespace Movies
{

    //Håndter alt UI
    public static class UI
    {
        //Menu'en til bruger
        public static void Menu() 
        {
            List<Film> movies = FilmManager.GetFilm();

            Console.Clear();
            
            Console.WriteLine(":::::::Start menu::::::::");

            Console.WriteLine();

            Console.WriteLine("-1: Exit");
            Console.WriteLine("0: Search Movie");
            Console.WriteLine("1: Search Actor");
            Console.WriteLine("2: Search Genre");
            Console.WriteLine("3: View all movie");
            Console.WriteLine("4: View all actors");

        }

        //Data for den valgte film
        public static void MovieViewer(int movie_id, List<Film> movies) 
        {
            Console.Clear();

            Console.WriteLine(":::::::Movie view::::::::");

            Console.WriteLine();

            Console.WriteLine($"Title: {movies[movie_id].Title}");

            Console.WriteLine($"Year: {movies[movie_id].Year}");

            Console.WriteLine();

            Console.WriteLine("Actors: ");

            List<string> actors = FilmManager.GetActorInMovies(movie_id, movies);

            for (int i = 0; i < actors.Count; i++)
            {
                Console.WriteLine(actors[i]);
            }

            Console.ReadKey();
        
        }

        //Data for den valgte skuespiller
        public static void ActorViewer(int actor_id, List<Actor> actors) 
        {
            Console.Clear();

            Console.WriteLine("::::::Actor Viewer::::::");

            Console.WriteLine();

            Console.WriteLine($"Name: {actors[actor_id].Firstname} {actors[actor_id].Lastname}");

            Console.WriteLine();

            Console.WriteLine("Played in: ");

            List<string> movies = FilmManager.GetFilmsFromActor(actor_id, actors);

            for (int i = 0; i < movies.Count; i++)
            {
                Console.WriteLine(movies[i]);
            }

            Console.ReadKey();

        }

        //Menu'en for at søge efter en film
        public static void SearchFilm() 
        {
            Console.Clear();

            Console.WriteLine("::::::Search Film:::::::");

            Console.WriteLine();

            Console.Write("Search: ");

            //Bruger søger efter film
            string searchInput = Console.ReadLine();
            List<Film> films = FilmManager.GetSearchFilm(searchInput);

            while (films.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid name");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                Console.Write("Search: ");

                searchInput = Console.ReadLine();
                films = FilmManager.GetSearchFilm(searchInput);
            }

            Console.WriteLine();

            for (int i = 0; i < films.Count; i++)
            {
                Console.WriteLine($"{i}: { films[i].Title}");
            }
            //Bruger kan valge en film ud fra det man har søgt
            UserManager.SearchSelectMovie(searchInput);

        
        }

        //Menu'en for at søge efter skuespiller
        public static void SearchActor()
        {
            Console.Clear();

            Console.WriteLine("::::::Search Actor:::::::");

            Console.WriteLine();

            Console.Write("Search: ");

            //Bruger søger efter skuespiller
            string searchInput = Console.ReadLine();

            List<Actor> actors = FilmManager.GetSearchActor(searchInput);

            while (actors.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid name");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                Console.Write("Search: ");

                searchInput = Console.ReadLine();
                actors = FilmManager.GetSearchActor(searchInput);
            }

            Console.WriteLine();

            for (int i = 0; i < actors.Count; i++)
            {
                Console.WriteLine($"{i}: { actors[i].Firstname} { actors[i].Lastname}");
            }

            //Bruger kan valge en skuespiller ud fra man har søgt
            UserManager.SearchSelectActor(searchInput);


        }

        //Menu'en for at søge efter genre
        public static void SearchGenre() 
        {
            Console.Clear();

            Console.WriteLine("::::::Search Genre:::::::");

            Console.WriteLine();

            List<string> genre = FilmManager.GetGenre();

            for (int i = 0; i < genre.Count; i++)
            {
                Console.WriteLine($"{i}: {genre[i]}");
            }
            //Brugeren kan valge en af de udskrevet genre
            string selectedGenre = UserManager.SelectGenre();

            Console.WriteLine();

            Console.WriteLine("Movies: ");

            List<Film> FilmByGenre = FilmManager.GetMoviesFromGenre(selectedGenre);

            for (int i = 0; i < FilmByGenre.Count; i++)
            {
                Console.WriteLine($"{i}: {FilmByGenre[i].Title}");
            }

            //Bruger kan valge en film ud for valgte af genre 
            UserManager.SelectMovieByGenre(FilmByGenre);

        }

        //Menu'en for at vise alle film og vægle en film
        public static void ViewAllFilms() 
        {
            Console.Clear();

            Console.WriteLine(":::::::All movies::::::::");

            List<Film> films = FilmManager.GetFilm();

            for (int i = 0; i < films.Count; i++)
            {
                Console.WriteLine($"{i}: {films[i].Title}");
            }

            //Bruger kan valge en af de udskrevet film
            UserManager.SelectMovie();


        }
        //Menu'en for at vise alle skuespiller og vægle en skuespiller
        public static void ViewAllActors()
        {
            Console.Clear();

            Console.WriteLine(":::::::All Actors::::::::");

            List<Actor> actors = FilmManager.GetActor();

            for (int i = 0; i < actors.Count; i++)
            {
                Console.WriteLine($"{i}: {actors[i].Firstname} {actors[i].Lastname}");
            }
            //Bruger kan valge en af de udskrevet skuespiller
            UserManager.SelectActor();

        }
    }
}
