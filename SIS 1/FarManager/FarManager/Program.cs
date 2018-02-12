using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarManager
{
    class Program
    {
        static void showInfo(FileSystemInfo[] current, int index)
        {
            drawBorder(); 
            int ind = 0;
            foreach (FileSystemInfo fsi in current)
            {
                Console.SetCursorPosition(1, ind+1);
                Console.BackgroundColor = ConsoleColor.Black;
                if (ind++ == index) Console.BackgroundColor = ConsoleColor.Red;
                String display = fsi.Name.Length < 20 ? fsi.Name : fsi.Name.Substring(0, 20);
                if (fsi is DirectoryInfo)
                {
                    draw("[+]", ConsoleColor.Green, false);
                    draw(display, ConsoleColor.Gray, true);
                    
                }
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        static void drawBorder()
        {
            Console.BackgroundColor = ConsoleColor.Black;
           // Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            for(int i = 0; i < Console.WindowHeight; i++)
            {
                for(int j = 0; j < Console.WindowWidth; j++)
                {
                    if (i == 0 || i == Console.WindowWidth - 1) {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.SetCursorPosition(i, j);
                        Console.Write("|");
                    }
                    else if (j == 0 || j == Console.WindowHeight - 1 || j == 44 || j == 34 || j == 29)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.SetCursorPosition(i, j);
                        Console.Write("=");
                        
                    }
                }
            }
            showPanel();
        }


        static void draw(string text, ConsoleColor cc, bool bl)
        {
            Console.ForegroundColor = cc;
            Console.Write(text);
            if (bl) Console.WriteLine();
        }

        static void draw(string text, ConsoleColor cc, ConsoleColor back, bool bl)
        {
            Console.BackgroundColor = back;
            draw(text, cc, bl);
        }

        static void showPanel()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(2, 47);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("F1 Help");
            Console.SetCursorPosition(10, 47);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("F2 MkFold");
            Console.SetCursorPosition(20, 47);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("F3 MkFil");
            Console.SetCursorPosition(29, 47);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("Del Remove");
            Console.SetCursorPosition(40, 47);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("F4 Searc");

        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(50, 50);
            Console.CursorVisible = false;
            Stack<FileSystemInfo[]> parent = new Stack<FileSystemInfo[]>();
            Stack<int> indexhist = new Stack<int>();
            string[] drives = Environment.GetLogicalDrives();
            FileSystemInfo[] cur = new FileSystemInfo[drives.Length];
            for (int i = 0; i < cur.Length; i++)
                cur[i] = new DirectoryInfo(drives[i]);

            int index = 0;
            showInfo(cur, index);

            ConsoleKeyInfo pressed = Console.ReadKey(true);
            while (pressed.Key != ConsoleKey.Escape)
            {
                switch (pressed.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (cur.Length > index + 1)
                            index++;
                        else index = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        if (index > 0) index--;
                        else index = cur.Length - 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.Clear();
                        if (parent.Count > 0)
                        {
                            index = indexhist.Pop();
                            cur = parent.Pop();
                        }
                        break;
                        
                    case ConsoleKey.RightArrow:
                        Console.Clear();
                        if (cur[index] is DirectoryInfo)
                        {
                            try
                            {
                                DirectoryInfo dir = cur[index] as DirectoryInfo;
                                indexhist.Push(index);
                                parent.Push(cur);
                                index = 0;
                                cur = dir.GetFileSystemInfos();
                            }

                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        else System.Diagnostics.Process.Start(cur[index].FullName);
                        break;
                }
                showInfo(cur, index);
                pressed = Console.ReadKey(true);
            }
        }
    }
}
