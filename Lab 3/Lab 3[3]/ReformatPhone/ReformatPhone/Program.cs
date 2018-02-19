using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReformatPhone
{
    class Program
    {
        static void Main(string[] args)
        {

            string phonePattern = @"^((\(\d{3}\))|(\d{3}))[\s\-]?\d{3}[\s\-]?\d{4}$";
            string zip = @"^(\d{4})(\-\d{4})?$";
            string email = @"^[a-z0-9_\.\-]+@[a-z]+\.[a-z]{2,6}$";
            string s;
            while (true)
            {
                s = Console.ReadLine();
                if (s == "q") break;
                else if (Regex.IsMatch(s, phonePattern))
                {
                    Console.WriteLine("Matching PHONE");
                    Console.WriteLine(ReformatPhone(s));
                }
                else if (Regex.IsMatch(s, zip))
                {
                    Console.WriteLine("Matching ZIP");
                }
                else if (Regex.IsMatch(s, email))
                {
                    Console.WriteLine("Matching EMAIL");
                }
                else Console.WriteLine("Not Matching");
            }
        }
            static string ReformatPhone(string s)
            {

                string pattern = @"\D+";
                string repl = "";
                Regex rg = new Regex(pattern);
                string ans = rg.Replace(s, repl);
                long l = long.Parse(ans);
                string answer = String.Format("{0:(###) ###-###}", l);



                return answer;

            }

        }
    }
