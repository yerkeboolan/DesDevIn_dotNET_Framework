using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortString
{
    class Program
    {
        static void Main(string[] args)
        {
            string sentence = "If you can do it";
            string[] words = sentence.Split(' ');

            string str = string.Join(" ", words);
            Console.WriteLine(str);

            //Array.Sort(words);
            /*for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine(words[i].ToString());
            }
            */
            /* foreach (string word in words)
            {
                Console.WriteLine(word);
            } */
            Console.ReadKey();
        }
    }
}
