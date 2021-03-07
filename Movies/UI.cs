using System;
using System.Collections.Generic;
using System.Linq;

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
            Console.WriteLine("3: View all Movie");
            Console.WriteLine("4: View all Actors");
            Console.WriteLine("5: Insert Actor");
            Console.WriteLine("6: Insert Movie");
            Console.WriteLine("7: Insert Genre");
            Console.WriteLine("8: Delete Movie");
            Console.WriteLine("9: Delete Actor");
            Console.WriteLine("10: Delete Genre");

        }

        public static void DeleteMovieView() 
        {
            List<Film> films = FilmManager.GetFilm();

            Console.Clear();

            Console.WriteLine("::::::Delete Movie::::::");

            Console.WriteLine();

            for (int i = 0; i < films.Count; i++)
            {
                Console.WriteLine($"{i}: {films[i].Title}");
            }

            InfoManager.DeleteMovie();
        }

        public static void DeleteActorView()
        {
            List<Actor> actors = FilmManager.GetActor();

            Console.Clear();

            Console.WriteLine("::::::Delete Actor::::::");

            Console.WriteLine();

            for (int i = 0; i < actors.Count; i++)
            {
                Console.WriteLine($"{i}: {actors[i].Firstname} {actors[i].Lastname}");
            }

            InfoManager.DeleteActor();
        }

        public static void DeleteGenreView()
        {
            List<Genre> genres = FilmManager.GetGenre();

            Console.Clear();

            Console.WriteLine("::::::Delete Genre::::::");

            Console.WriteLine();

            for (int i = 0; i < genres.Count; i++)
            {
                Console.WriteLine($"{i}: {genres[i].Name}");
            }

            InfoManager.DeleteGenre();
        }

        //Data for den valgte film
        public static void MovieViewer(int movie_id, List<Film> movies) 
        {
            List<Film> selectedFilm = FilmManager.GetFilm().Where(x => x.Id == movie_id).ToList();

            Console.Clear();

            Console.WriteLine(":::::::Movie view::::::::");

            Console.WriteLine();

            Console.WriteLine($"Title: {selectedFilm[0].Title}");

            Console.WriteLine($"Year: {selectedFilm[0].Year}");

            Console.Write($"Genre: ");
            selectedFilm[0].Genre.ForEach(x => Console.Write(x.Name + " "));
            Console.WriteLine();

            Console.WriteLine("Actors: ");

            List<Actor> actors = FilmManager.GetActorInMovies(movie_id, movies);

            for (int i = 0; i < actors.Count; i++)
            {
                Console.WriteLine(actors[i].Firstname + " " + actors[i].Lastname);
            }

            Console.WriteLine();
            Console.WriteLine("--Edit--");
            Console.WriteLine("0. Back to menu");
            Console.WriteLine("1. Add Actor");
            Console.WriteLine("2. Delete Actor");
            Console.WriteLine("3. Edit info");
            Console.WriteLine("4. Add Genre");
            Console.WriteLine("5. Delete Genre");

            MovieEditManager.Movie_id = movie_id;
            MovieEditManager.Movies = movies;

            MovieEditManager.FilmEditMenu();

            Console.ReadKey();
        
        }

        //Data for den valgte skuespiller
        public static void ActorViewer(int actor_id, List<Actor> actors) 
        {
            List<Actor> selectedActor = actors.Where(x => x.Id == actor_id).ToList();

            Console.Clear();

            Console.WriteLine("::::::Actor Viewer::::::");

            Console.WriteLine();

            Console.WriteLine($"Name: {selectedActor[0].Firstname} {selectedActor[0].Lastname}");

            Console.WriteLine();

            Console.WriteLine("Played in: ");

            List<Film> movies = FilmManager.GetFilmsFromActor(actor_id, actors);

            for (int i = 0; i < movies.Count; i++)
            {
                Console.WriteLine(movies[i].Title);
            }

            Console.WriteLine();
            Console.WriteLine("--Edit--");
            Console.WriteLine("0. Back to menu");
            Console.WriteLine("1. Add Film");
            Console.WriteLine("2. Delete Film");
            Console.WriteLine("3. Edit info");

            ActorEditManager.Actor_id = actor_id;
            ActorEditManager.Actors = actors;

            ActorEditManager.ActorEditMenu();

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
            InfoManager.SearchSelectMovie(searchInput);

        
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
            InfoManager.SearchSelectActor(searchInput);


        }

        //Menu'en for at søge efter genre
        public static void SearchGenre() 
        {
            Console.Clear();

            Console.WriteLine("::::::Search Genre:::::::");

            Console.WriteLine();

            List<Genre> genre = FilmManager.GetGenreName();

            for (int i = 0; i < genre.Count; i++)
            {
                Console.WriteLine($"{i}: {genre[i].Name}");
            }
            //Brugeren kan valge en af de udskrevet genre
            Genre selectedGenre = FilmManager.GetGenreName()[InfoManager.SelectGenre()];

            Console.WriteLine();

            Console.WriteLine("Movies: ");

            List<Film> FilmByGenre = FilmManager.GetMoviesFromGenre(selectedGenre);

            for (int i = 0; i < FilmByGenre.Count; i++)
            {
                Console.WriteLine($"{i}: {FilmByGenre[i].Title}");
            }

            //Bruger kan valge en film ud for valgte af genre 
            InfoManager.SelectMovieByGenre(FilmByGenre);

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
            InfoManager.SelectMovie();


        }
        //Menu'en for at vise alle skuespiller og vægle en skuespiller
        public static void ViewAllActors()
        {
            Console.Clear();

            Console.WriteLine(":::::::All Actors::::::::");

            Console.WriteLine();

            List<Actor> actors = FilmManager.GetActor();

            for (int i = 0; i < actors.Count; i++)
            {
                Console.WriteLine($"{i}: {actors[i].Firstname} {actors[i].Lastname}");
            }
            //Bruger kan valge en af de udskrevet skuespiller
            InfoManager.SelectActor();

        }


        public static void InsertActorView() 
        {
            Console.Clear();

            Console.WriteLine(":::::::Insert Actor:::::::::");

            Console.WriteLine();

            Console.Write("First name: ");

            string firstname = Console.ReadLine();

            Console.WriteLine();

            Console.Write("Last name: ");

            string lastname = Console.ReadLine();

            InfoManager.InsertActor(firstname,lastname);
        }

        public static void InsertMovieView()
        {
            Console.Clear();

            Console.WriteLine(":::::::Insert Movie:::::::::");

            Console.WriteLine();

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.WriteLine();

            Console.Write("Year: ");
            int year = InfoManager.ParseInt();

            Console.WriteLine();

            Console.WriteLine("Genre there exist: ");
            List<Genre> genre = FilmManager.GetGenreName();

            for (int i = 0; i < genre.Count; i++)
            {
                Console.WriteLine($"{i}: {genre[i].Name}");
            }
            Console.WriteLine();
            //Brugeren kan valge en af de udskrevet genre
            int selectedGenre = InfoManager.SelectGenre();

            InfoManager.InsertFilm(title,year,selectedGenre);

        }

        public static void InsertGenreView() 
        {
            Console.Clear();

            Console.WriteLine(":::::::Insert Genre:::::::::");

            Console.WriteLine();

            Console.Write("Name: ");
            string name = Console.ReadLine();

            FilmManager.InsertGenre(new Genre(name));

        }

        public static void ErrorMessage() 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid number");
            Console.ResetColor();
        }

        public static void Message(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
