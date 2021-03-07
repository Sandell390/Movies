using System;
using System.Collections.Generic;


namespace Movies
{
    //Lave en ny class: EditManger; Nyt navn til denne class: InfoManager
    //Håndter alt bruger input
    public static class InfoManager
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
                case 5:
                    UI.InsertActorView();
                    return true;
                case 6:
                    UI.InsertMovieView();
                    return true;
                case 7:
                    UI.InsertGenreView();
                    return true;
                case 8:
                    UI.DeleteMovieView();
                    return true;
                case 9:
                    UI.DeleteActorView();
                    return true;
                case 10:
                    UI.DeleteGenreView();
                    return true;
                default:
                    UI.ErrorMessage();
                    return true;
            }

        }


        public static void DeleteMovie() 
        {
            List<Film> films = FilmManager.GetFilm();

            while (true)
            {
                int input = ParseInt();
                if (input < films.Count && input >= 0) 
                {
                    FilmManager.DeleteMovie(films[input].Id);
                    break;
                }
                else
                {
                    UI.ErrorMessage();
                }
            }
        }

        public static void DeleteActor()
        {
            List<Actor> actors = FilmManager.GetActor();

            while (true)
            {
                int input = ParseInt();
                if (input < actors.Count && input >= 0)
                {
                    FilmManager.DeleteActor(actors[input].Id);
                    break;
                }
                else
                {
                    UI.ErrorMessage();
                }
            }
        }

        public static void DeleteGenre()
        {
            List<Genre> genres = FilmManager.GetGenre();

            while (true)
            {
                int input = ParseInt();
                if (input < genres.Count && input >= 0)
                {
                    FilmManager.DeleteGenre(genres[input].Id);
                    break;
                }
                else
                {
                    UI.ErrorMessage();
                }
            }
        }

        //Tjekker om det er en int
        public static int ParseInt() 
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                UI.ErrorMessage();
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
                    UI.MovieViewer(searchedFilms[input].Id, searchedFilms);
                    break;
                }
                else
                {
                    UI.ErrorMessage();
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
                if (input < searchedActors.Count && input >= 0)
                {
                    //Viser den skuespiller man har valgt
                    UI.ActorViewer(searchedActors[input].Id, searchedActors);
                    break;
                }
                else
                {
                    UI.ErrorMessage();
                }
            }


        }

        //Bruger skal vægle en genre
        public static int SelectGenre() 
        {
            //Får alle genre
            List<Genre> genre = FilmManager.GetGenreName();
            while (true)
            {
                int input = ParseInt();
                if (input < genre.Count && input >= 0)
                {
                    //Sender genre navn tilbage
                    return input;
                }
                else
                {
                    UI.ErrorMessage();
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
                    UI.MovieViewer(movies[input].Id, movies);
                    break;
                }
                else
                {
                    UI.ErrorMessage();
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
                    UI.ActorViewer(actors[input].Id, actors);
                    break;
                }
                else
                {
                    UI.ErrorMessage();
                }
            }
        }

        //Bruger har valgt en genre og skal til at vægle en film fra den valgte genre 
        public static void SelectMovieByGenre(List<Film> films) 
        {
            if (films.Count != 0) 
            {
                while (true)
                {
                    int input = ParseInt();
                    if (input < films.Count && input >= 0)
                    {
                        //Viser den film man valgt
                        UI.MovieViewer(films[input].Id, films);
                        break;
                    }
                    else
                    {
                        UI.ErrorMessage();
                    }
                }
                
            }
            else
            {
                UI.Message("There is no movies in this genre");
            }
            

        }

        public static void InsertActor(string firstname, string lastname)
        {
            Actor a = FilmManager.InsertActor(new Actor(firstname, lastname));

            List<Actor> actors = FilmManager.GetActor();

            UI.ActorViewer(a.Id, actors);

        }

        public static void InsertFilm(string title, int year, int genre)
        {
            List<Genre> genres = FilmManager.GetGenre();

            Film a = FilmManager.InsertFilm(new Film(title, year, new List<Genre>()), genres[genre].Id);

            List<Film> films = FilmManager.GetFilm();

            UI.MovieViewer(a.Id, films);

        }

    }
}
