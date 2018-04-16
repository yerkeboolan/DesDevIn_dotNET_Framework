using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizAboutMovie.Controllers
{
    public class MovieController : Controller
    {
        // GET: ListOfMovies/random
        public ActionResult Random()
        {
            List<Models.Movie> lom = new List<Models.Movie>();
            FileStream fs = new FileStream(@"C:\Users\Yerkebulan\Downloads\Telegram Desktop\listofmovies.txt", FileMode.OpenOrCreate, FileAccess.Read);

            StreamReader reader = new StreamReader(fs);
            string line = reader.ReadToEnd();
            reader.Close();
            fs.Close();
            string[] inputs = line.Split('\n');

            for (int i = 0; i < inputs.Length; i++)
            {
                string[] inputs2 = inputs[i].Split('+');
                lom.Add(new Models.Movie { Year = (inputs2[2]), Rating = (inputs2[0]), Name = inputs2[1] });

            }

            Random rnd = new System.Random();

            var result = lom.OrderBy(x => rnd.Next()).Take(3).ToList();
            return View(result);
        }
    }
}