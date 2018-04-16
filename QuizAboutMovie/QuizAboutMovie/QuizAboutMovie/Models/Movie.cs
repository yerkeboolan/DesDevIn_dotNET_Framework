using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizAboutMovie.Models
{
    public class Movie
    {
        public static int ID_COUNT = 0;
        public int ID { get; }
        public string Rating { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }

        public Movie()
        {
            ID = ++ID_COUNT;
        }
    }
}