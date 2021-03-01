

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

        private string genre;

        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }




        public Film(int _id, string _title, int _year,string _genre) 
        {
            id = _id;
            title = _title;
            year = _year;
            genre = _genre;
        }


        
    }
}
