using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable hashTable = new Hashtable();
            ListDictionary listDicti = new ListDictionary();
            HybridDictionary hybDicti = new HybridDictionary();


            Stopwatch stopWatchHashTable = new Stopwatch();
            Stopwatch stopWatchListDicti = new Stopwatch();
            Stopwatch stopWatchHybDicti = new Stopwatch();

            int cnt = 1000, hashCnt = 0, listCnt = 0, hybCnt = 0;
            for (int k = 0; k < 999; k++)
            {
                stopWatchHashTable.Start();
                for (int i = 0; i < cnt; i++) hashTable.Add(i, "DesDevIn_dotNET");

                hashTable.Clear();
                stopWatchHashTable.Stop();
                TimeSpan tsHashTable = stopWatchHashTable.Elapsed;
                hashCnt += tsHashTable.Milliseconds;
            }

            Console.WriteLine("RunTime HashTable " + hashCnt / 1000);
            for (int k = 0; k < 999; k++)
            {
                stopWatchListDicti.Start();
                for (int i = 0; i < cnt; i++) listDicti.Add(i, "DesDevIn_dotNET");

                listDicti.Clear();
                stopWatchListDicti.Stop();
                TimeSpan tsListDicti = stopWatchListDicti.Elapsed;
                listCnt += tsListDicti.Milliseconds;
            }

            Console.WriteLine("RunTime ListDicti " + listCnt / 1000);
            for (int k = 0; k < 999; k++)
            {
                stopWatchHybDicti.Start();
                for (int i = 0; i < cnt; i++) hybDicti.Add(i, "DesDevIn_dotNET");

                hybDicti.Clear();
                stopWatchHybDicti.Stop();
                TimeSpan tsHybDicti = stopWatchHybDicti.Elapsed;

                hybCnt += tsHybDicti.Milliseconds;
            }
            
            Console.WriteLine("RunTime HybDicti " + hybCnt / 1000);

            Console.ReadKey();

        }
    }
}
