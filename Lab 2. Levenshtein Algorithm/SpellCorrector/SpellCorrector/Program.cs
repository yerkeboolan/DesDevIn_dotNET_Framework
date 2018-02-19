using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpellCorrector
{
    class Program
    {
        static void Main(string[] args)
        {
            string s1, s2;
            s1 = Console.ReadLine();
            s2 = Console.ReadLine();
              
                int[,] dp = new int[s1.Length + 1, s2.Length + 1];
                for (int i = 0; i <= s1.Length; i++)
                {
                    dp[i, 0] = i;
                }
                for (int j = 0; j <= s2.Length; j++)
                {
                    dp[0, j] = j;
                }

                for (int i = 1; i <= s1.Length; i++)
                {
                    for (int j = 1; j <= s2.Length; j++)
                    {
                        if (s1[i - 1] == s2[j - 1])
                        {
                            dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1]);
                        }
                        else
                        {
                            dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + 2);
                        }
                    }
                }
               
            Console.WriteLine(dp[s1.Length, s2.Length]);
 
            
            Console.ReadKey();
        }
    }
}
