using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ViewTextFiles
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                StreamReader tr = new StreamReader(@"C:\user\wow.txt");
                try
                {
                    Console.WriteLine(tr);
                }
                catch (Exception u)
                {
                    Console.WriteLine(u);
                }
                tr.Close();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Sorry, you lack sufficient privileges.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Sorry, the file does not exist.");
            }
            Console.ReadKey();
        }
    }
}
