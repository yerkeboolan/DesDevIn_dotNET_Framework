using RandomMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace RandomMovie.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        // GET: Movies
        
        StreamReader sr = new StreamReader(@"C:\Users\Yerkebulan\Desktop\c# quiz\listofmovies.txt");
        public static List<Movie> list = new List<Models.Movie>();

        Random rand1 = new Random();

        // GET: Movie
        public ActionResult Random()
        {
            String st = sr.ReadToEnd();

            string name2 = @"([?\\[+A - Z +\\]])";

            string date = @"\((\d{4})\)";
            sr.Close();

            MatchCollection match1 = Regex.Matches(st, name2);
            MatchCollection match3 = Regex.Matches(st, date);

            for (int i = 1; i < 1001; i++)
            {
                list.Add(new Movie() { ID = i });
            }

            foreach (Match m in match1)
            {
                list.Add(new Movie() { Name = m.Groups[1].Value });
            }
            foreach (Match m in match3)
            {
                list.Add(new Movie() { Year = int.Parse(m.Groups[1].Value) });
            }

            int f = rand1.Next(1, 1000);
            int s = rand1.Next(1, 1000);
            int t = rand1.Next(1, 1000);


            var res = list.OrderBy(n => n.Name).Select(n => n).Where(n => n.ID == f || n.ID == s || n.ID == t).ToList();

            return View(res);
        }
    }
    


























    /*
string[] text = System.IO.File.ReadAllLines(@"C:\Users\Yerkebulan\Desktop\c# quiz\listofmovies.txt");     

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
    Random rnd = new System.Random();

    foreach (string line in text)
    {
                var m = new Movie()
                {
                    ID = Int32.Parse(GetID(line)),
                    Rating = Double.Parse(Rating(line)),
                    Name = Name(line),
                    Year = Int32.Parse(Year(line))
                };
                    
        movies.Add(m);
    }

    for (int i = 0; i > 3; i++)
    {

        randomov_3.Add(movies[rnd.Next(0, movies.Count)]);
    }
    return View(randomov_3);
}
}*/
}