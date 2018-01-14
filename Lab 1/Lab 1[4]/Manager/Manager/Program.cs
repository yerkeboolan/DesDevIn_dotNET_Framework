using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Manager manager = new Manager("Dariya", "Yersaiynkyzy", 14, Genders.Female, "87774444060", "microregion Kulager");

            Console.WriteLine(manager);
            Console.ReadKey();
        }
    }
}
