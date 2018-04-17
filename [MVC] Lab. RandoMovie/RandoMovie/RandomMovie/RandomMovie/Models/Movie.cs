using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RandomMovie.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public Double Rating { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
}