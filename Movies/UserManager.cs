using System;
using System.Collections.Generic;


namespace Movies
{
    //Håndter alt bruger input
    public static class UserManager
    {
        public static bool MenuSelect() 
        {
            //Vælger noget for menu'en
            int input = ParseInt();

            switch (input)
            {
                case -1:
                    return false;
                case 0:
                    UI.SearchFilm();
                    return true;
                case 1:
                    UI.SearchActor();
                    return true;
                case 2:
                    UI.SearchGenre();
                    return true;
                case 3:
                    UI.ViewAllFilms();
                    return true;
                case 4:
                    UI.ViewAllActors();
                    return true;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid number");
                    Console.ResetColor();
                    Console.ReadKey();
                    return true;
            }

        }

        //Tjekker om det er en int
        static int ParseInt() 
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number");
                Console.ResetColor();
            }
            return input;

        }

        //Bruger har søgt efter film og skal nu til at vægle en film ud for hvad man har søgt
        public static void SearchSelectMovie(string searchInput) 
        {
            
            while (true)
            {
                //Får film for det man har søgt på
                List<Film> searchedFilms = FilmManager.GetSearchFilm(searchInput);
                int input = ParseInt();
                if (input < searchedFilms.Count && input >= 0)
                {
                    //Viser den film man har valgt
                    UI.MovieViewer(input, searchedFilms);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid number");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
            

        }

        //Bruger har søgt efter en skuespiller og skal nu til at vægle en skuespiller ud for hvad man har søgt
        public static void SearchSelectActor(string searchInput)
        {

            while (true)
            {
                //Får skuespiller for det man har søgt på
                List<Actor> searchedActors = FilmManager.GetSearchActor(searchInput);
                int input = ParseInt();
                if (input < searchedActors.Count && input > 0)
                {
                    //Viser den skuespiller man har valgt
                    UI.ActorViewer(input, searchedActors);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid number");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }


        }

        //Bruger skal vægle en genre
        public static string SelectGenre() 
        {
            //Får alle genre
            List<string> genre = FilmManager.GetGenre();
            while (true)
            {
                int input = ParseInt();
                if (input < genre.Count && input >= 0)
                {
                    //Sender genre navn tilbage
                    return genre[input];
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid number");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
        }

        //Bruger skal vægle en film
        public static void SelectMovie() 
        {
            //Får alle film
            List<Film> movies = FilmManager.GetFilm();
            while (true)
            {
                int input = ParseInt();
                if (input < movies.Count && input >= 0)
                {
                    //Viser den film man har valgt
                    UI.MovieViewer(input, movies);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid number");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }

        }

        //Bruger skal vægle en skuespiller
        public static void SelectActor() 
        {
            //Får alle skuespiller
            List<Actor> actors = FilmManager.GetActor();
            while (true)
            {
                int input = ParseInt();
                if (input < actors.Count && input >= 0)
                {
                    //Viser den skuespiller man har valgt
                    UI.ActorViewer(input, actors);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid number");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
        }

        //Bruger har valgt en genre og skal til at vægle en film fra den valgte genre 
        public static void SelectMovieByGenre(List<Film> films) 
        {
            while (true)
            {
                int input = ParseInt();
                if (input < films.Count && input >= 0)
                {
                    //Viser den film man valgt
                    UI.MovieViewer(input, films);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid number");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }

        }



    }
}
