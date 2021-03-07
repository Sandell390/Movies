using System.Collections.Generic;
using System.Linq;

namespace Movies
{
    //Indeholder alt data fra tablet actor i databasen
    public class Actor
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string firstname;

        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }


        private string lastname;

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }


        public Actor() 
        { 
        
        
        }

        public Actor(int _id, string _firstname, string _lastname)
        {
            id = _id;
            firstname = _firstname;
            lastname = _lastname;


        }

        public Actor(string _firstname, string _lastname)
        {
            firstname = _firstname;
            lastname = _lastname;

        }

    }
}
