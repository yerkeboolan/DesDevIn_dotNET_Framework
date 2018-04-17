using RandomMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RandomMovie.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        string[] text = System.IO.File.ReadAllLines(@"C:\Users\Yerkebulan\Downloads\Telegram Desktop\listofmovies.txt");
        List<Movie> movies = new List<Movie>();
        List<Movie> randomov_3 = new List<Movie>();

        string GetID(string line)
        {
            string id = line.Substring(6, 18);
            string idString = id.Replace(" ", "");
            return idString;
        }
        string Rating(string line)
        {
            string rating = line.Substring(27, 3);
            string ratingString = rating.Replace(" ", "");
            return ratingString;
        }
        string Name(string line)
        {
            string name = "";
            string nameEx = line.Substring(32);
            for (int i = 0; i < nameEx.Length; i++)
            {
                if (nameEx[i] == ' ' || Char.IsLetter(nameEx[i]))
                {
                    name += nameEx[i];
                }
            }
            return name;
        }
        string Year(string line)
        {

            string given = line.Substring(line.Length - 7);
            string result = given.Replace("(", "");
            string result2 = result.Replace(")", "");
            return result2;
        }

        public ActionResult Random()
        {
            foreach (string line in text)
            {
                var m = new Movie() { ID = Int32.Parse(GetID(line)), Rating = Double.Parse(Rating(line)), Name = Name(line), Year = Int32.Parse(Year(line)) };
                movies.Add(m);
            }

            Random rnd = new System.Random();

            movies.OrderBy(x => rnd.Next()).Take(3);
            for (int i = movies.Count - 1; i > movies.Count - 6; i--)
            {
                randomov_3.Add(movies[i]);
            }
            return View(randomov_3);
        }
    }
}