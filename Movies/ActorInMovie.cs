using System;
using System.Collections.Generic;


namespace Movies
{
    //Indeholder data fra tablet Movie_actor N-M fra databasen
    public class ActorInMovie
    {
        private int movie_id;

        public int Movie_id
        {
            get { return movie_id; }
            set { movie_id = value; }
        }

        private int actor_id;

        public int Actor_id
        {
            get { return actor_id; }
            set { actor_id = value; }
        }

        public ActorInMovie() 
        { 
        
        }

        public ActorInMovie(int _movieId, int _actorId) 
        {
            movie_id = _movieId;
            actor_id = _actorId;
        }


    }
}
