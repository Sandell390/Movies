using System;

namespace Movies
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            bool power = true;
            while (power)
            {
                UI.Menu();

                power = InfoManager.MenuSelect();
            }

        }
    }
}
