using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    public static class ActorEditManager
    {
        private static int actor_id;

        public static int Actor_id
        {
            get { return actor_id; }
            set { actor_id = value; }
        }

        private static List<Actor> actors;

        public static List<Actor> Actors
        {
            get { return actors; }
            set { actors = value; }
        }

        public static void ActorEditMenu()
        {

            while (true)
            {
                int input = InfoManager.ParseInt();

                switch (input)
                {
                    case 0:
                        break;
                    case 1:
                        AddFilm();
                        break;
                    case 2:
                        DeleteFilm();
                        break;
                    case 3:
                        EditActorInfo();
                        break;
                    default:
                        UI.ErrorMessage();
                        break;
                }

                if (input <= 3 && input > 0)
                {
                    UI.ActorViewer(actor_id, Actors);
                    break;
                }
                if (input == 0)
                {
                    break;
                }
            }

        }

        static void EditActorInfo()
        {
            Actor selectedActor = actors.Find(x => x.Id == actor_id);

            Console.WriteLine();

            string editedFirstname = selectedActor.Firstname;

            string editedLastname = selectedActor.Lastname;

            Console.WriteLine("Do you want to edit the firstname? 1. Yes 2. No");

            while (true)
            {
                int input = InfoManager.ParseInt();

                if (input == 1)
                {
                    Console.WriteLine();

                    Console.Write("New Firstname: ");
                    editedFirstname = Console.ReadLine();
                    break;
                }
                else if (input == 2)
                {
                    break;
                }
            }

            Console.WriteLine();

            Console.WriteLine("Do you want to edit the lastname? 1. Yes 2. No");

            while (true)
            {
                int input = InfoManager.ParseInt();

                if (input == 1)
                {
                    Console.WriteLine();

                    Console.Write("New Lastname: ");
                    editedLastname = Console.ReadLine();
                    break;
                }
                else if (input == 2)
                {
                    break;
                }
            }

            FilmManager.UpdateActorData(new Actor(actor_id, editedFirstname, editedLastname));
        }

        static void AddFilm() 
        {
            Console.WriteLine();

            List<Film> playedFilm = FilmManager.GetFilmsFromActor(Actor_id,Actors);
            List<Film> films = FilmManager.GetFilm();

            List<Film> missingfilms = new();

            missingfilms.AddRange(films);

            for (int i = 0; i < films.Count; i++)
            {
                for (int j = 0; j < playedFilm.Count; j++)
                {
                    if (films[i].Id == playedFilm[j].Id)
                    {
                        missingfilms.Remove(films[i]);
                        break;
                    }
                }
            }

            for (int i = 0; i < missingfilms.Count; i++)
            {
                Console.WriteLine($"{i}: {missingfilms[i].Title}");
            }

            while (true)
            {
                int input = InfoManager.ParseInt();
                if (input < missingfilms.Count && input >= 0)
                {
                    FilmManager.InsertActorInMovie(new ActorInMovie(missingfilms[input].Id,actor_id));
                    break;
                }
                else
                {
                    UI.ErrorMessage();
                }
            }
        }

        static void DeleteFilm()
        {
            Console.WriteLine();

            List<Film> films = FilmManager.GetFilmsFromActor(Actor_id, Actors);

            for (int i = 0; i < films.Count; i++)
            {
                Console.WriteLine($"{i}: {films[i].Title}");
            }

            while (true)
            {
                int input = InfoManager.ParseInt();
                if (input < actors.Count && input >= 0)
                {
                    FilmManager.DeleteActorInMovie(new ActorInMovie(films[input].Id, actor_id));
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
