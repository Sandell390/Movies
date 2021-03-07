using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    public class Genre
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Genre() 
        { 
        
        }
        public Genre(int _id, string _name)
        {
            id = _id;
            name = _name;
        }

        public Genre(string _name)
        {
            name = _name;
        }

    }
}
