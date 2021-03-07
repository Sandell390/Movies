using System.Collections.Generic;

namespace Movies
{
    //Indeholder alt data fra tablet movie i databasen
    public class Film
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private int year;

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        private List<Genre> genre;

        public List<Genre> Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        public Film(int _id, string _title, int _year, List<Genre> _genre) 
        {
            id = _id;
            title = _title;
            year = _year;
            genre = _genre;
        }

        public Film(string _title, int _year, List<Genre> _genre)
        {
            title = _title;
            year = _year;
            genre = _genre;
        }

        public Film(int _id, string _title, int _year)
        {
            id = _id;
            title = _title;
            year = _year;
        }
    }
}
