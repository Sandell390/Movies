using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Movies
{
    public static class MovieEditManager
    {
        private static int movie_id;

        public static int Movie_id
        {
            get { return movie_id; }
            set { movie_id = value; }
        }

        private static List<Film> movies;

        public static List<Film> Movies
        {
            get { return movies; }
            set { movies = value; }
        }



        public static void FilmEditMenu()
        {

            while (true)
            {
                int input = InfoManager.ParseInt();

                switch (input)
                {
                    case 0:
                        break;
                    case 1:
                        AddActorInMovie();
                        break;
                    case 2:
                        DeleteActorInMovie();
                        break;
                    case 3:
                        EditMovieInfo();
                        break;
                    case 4:
                        AddGenreToMovie();
                        break;
                    case 5:
                        DeleteGenreFromMovie();
                        break;
                    default:
                        UI.ErrorMessage();
                        break;
                }

                if (input <= 5 && input > 0)
                {
                    UI.MovieViewer(movie_id, movies);
                    break;
                }
                if (input == 0) 
                {
                    break;
                }
            }

        }

        static void DeleteGenreFromMovie() 
        {
            Console.WriteLine();

            List<Genre> genreInMovie = movies.Find(x => x.Id == movie_id).Genre;

            for (int i = 0; i < genreInMovie.Count; i++)
            {
                Console.WriteLine($"{i}: {genreInMovie[i].Name}");
            }

            while (true)
            {
                int input = InfoManager.ParseInt();
                if (input < genreInMovie.Count && input >= 0)
                {
                    FilmManager.DeleteGenreFromMovie(genreInMovie[input], Movie_id);
                    break;
                }
                else
                {
                    UI.ErrorMessage();
                }
            }

        }
        static void AddGenreToMovie() 
        {
            Console.WriteLine();
            
            List<Genre> genres = FilmManager.GetGenre();
            List<Genre> genreInMovie = movies.Find(x => x.Id == movie_id).Genre;


            List<Genre> missingGenre = new();

            missingGenre.AddRange(genres);

            for (int i = 0; i < genres.Count; i++)
            {
                for (int j = 0; j < genreInMovie.Count; j++)
                {
                    if (genres[i].Id == genreInMovie[j].Id)
                    {
                        missingGenre.Remove(genres[i]);
                        break;
                    }
                }
            }

            for (int i = 0; i < missingGenre.Count; i++)
            {
                Console.WriteLine($"{i}: {missingGenre[i].Name}");
            }

            while (true)
            {
                int input = InfoManager.ParseInt();
                if (input < missingGenre.Count && input >= 0)
                {
                    FilmManager.InsertGenreMovie(Movie_id,missingGenre[input].Id);
                    break;
                }
                else
                {
                    UI.ErrorMessage();
                }
            }

        }
        static void EditMovieInfo() 
        {
            List<Film> selectedFilm = movies.Where(x => x.Id == movie_id).ToList();

            Console.WriteLine();

            string editedTitle = selectedFilm[0].Title;

            int editedYear = selectedFilm[0].Year;

            Console.WriteLine("Do you want to edit the Title? 1. Yes 2. No");

            while (true)
            {
                int input = InfoManager.ParseInt();

                if(input == 1) 
                {
                    Console.WriteLine();

                    Console.Write("New Title: ");
                    editedTitle = Console.ReadLine();
                    break;
                }
                else if(input == 2)
                {
                    break;
                }
            }

            Console.WriteLine();

            Console.WriteLine("Do you want to edit the Year? 1. Yes 2. No");

            while (true)
            {
                int input = InfoManager.ParseInt();

                if (input == 1)
                {
                    Console.WriteLine();

                    Console.Write("New Year: ");
                    editedYear = InfoManager.ParseInt();
                    break;
                }
                else if (input == 2)
                {
                    break;
                }
            }

            FilmManager.UpdateFilmData(new Film(_id: movie_id, editedTitle, editedYear)) ;
        }
        static void DeleteActorInMovie() 
        {
            Console.WriteLine();

            List<Actor> actors = FilmManager.GetActorInMovies(movie_id, movies);

            for (int i = 0; i < actors.Count; i++)
            {
                Console.WriteLine($"{i}: {actors[i].Firstname} {actors[i].Lastname}");
            }

            while (true)
            {
                int input = InfoManager.ParseInt();
                if (input < actors.Count && input >= 0)
                {
                    FilmManager.DeleteActorInMovie(new ActorInMovie(movie_id, actors[input].Id));
                    break;
                }
                else
                {
                    UI.ErrorMessage();
                }
            }

        }

        static void AddActorInMovie() 
        {
            Console.WriteLine();

            List<Actor> actorsInFilm = FilmManager.GetActorInMovies(movie_id, movies);
            List<Actor> actors = FilmManager.GetActor();

            List<Actor> missingActors = new();

            missingActors.AddRange(actors);

            for (int i = 0; i < actors.Count; i++)
            {
                for (int j = 0; j < actorsInFilm.Count; j++)
                {
                    if (actors[i].Id == actorsInFilm[j].Id) 
                    {
                        missingActors.Remove(actors[i]);
                        break;
                    }
                }
            }
            
            for (int i = 0; i < missingActors.Count; i++)
            {
                Console.WriteLine($"{i}: {missingActors[i].Firstname} {missingActors[i].Lastname}");
            }

            while (true)
            {
                int input = InfoManager.ParseInt();
                if (input < missingActors.Count && input >= 0)
                {
                    FilmManager.InsertActorInMovie(new ActorInMovie(movie_id, missingActors[input].Id));
                    break;
                }
                else
                {
                    UI.ErrorMessage();
                }
            }


        }
        
    }
}
