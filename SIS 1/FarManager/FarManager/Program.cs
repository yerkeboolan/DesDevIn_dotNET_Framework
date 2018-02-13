using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

namespace FarManager
{
    class Program
    {
        static bool ok = false;
        enum Create { File, Folder, NoN, Search }
        static Create create = Create.NoN;
        static string folderName = "New folder";
        static string fileName = "NewFile.txt";
        static string searchedWord = "";
        static string pathFile = "", pathFolder = "";
        static string nameFile = "", nameFolder = "";
        static bool cClicked = false, mClicked = false;
        static List<FileSystemInfo> searchedFsis = new List<FileSystemInfo>();
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
            //Console.Clear();
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
            showInfoPattern();
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
            drawBorder();
            showInfo(cur, index);
            ShowInfo(cur[index]);
            bool show = true;
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
                    case ConsoleKey.F1:
                        if (show)
                        {
                            ShowHelp();
                            show = false;
                            Console.SetCursorPosition(2, 47);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("F1 Help");
                        }
                        else
                        {
                            show = true;
                            showPanel();
                            showInfoPattern();
                        }
                        break;
                    case ConsoleKey.F2:
                        Console.SetCursorPosition(10, 47);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("F2 MkFold");
                        create = Create.Folder;
                        EnterWord();

                        break;
                    case ConsoleKey.F3:
                        Console.SetCursorPosition(20, 47);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("F3 MkFil");
                        create = Create.File;
                        EnterWord();
                        break;
                    case ConsoleKey.F4:
                        Console.SetCursorPosition(40, 47);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("F4 Searc");
                        create = Create.Search;
                        EnterWord();
                        break;
                  /*  case ConsoleKey.Delete:
                        if (parent.Count > 1 && indexhist.Count > 1)
                        {
                            try
                            {
                                DialogResult ans = new DialogResult();
                                if (cur[index] is DirectoryInfo) { ans = MessageBox.Show("Are you sure, you want to delete current folder and all its insides?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question); }
                                else { ans = MessageBox.Show("Are you sure, you want to delete current file?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question); }

                                if (ans == DialogResult.Yes)
                                {
                                    if (cur[index] is DirectoryInfo) Directory.Delete(cur[index].FullName, true);
                                    else cur[index].Delete();
                                }

                            }
                            catch (Exception e) { }
                            cur = (parent.First()[indexhist.First()] as DirectoryInfo).GetFileSystemInfos();
                            Clear(28);
                            if (index >= cur.Length) index--;
                        }

                        break; */
                    case ConsoleKey.C:
                        cClicked = true;
                        if (cur[index] is FileInfo)
                        {
                            pathFile = parent.First()[indexhist.First()].FullName;
                            nameFile = cur[index].Name;
                        }
                        else
                        {
                            pathFolder = parent.First()[indexhist.First()].FullName;
                            nameFolder = cur[index].Name;
                        }
                        break;
                    case ConsoleKey.Enter:
                        showPanel();

                        if (parent.Count > 0 && indexhist.Count > 0)
                        {
                            if (create == Create.Search)
                            {

                                Search(searchedWord, parent.First()[indexhist.First()] as DirectoryInfo);
                                if (cur[index] is DirectoryInfo)
                                {
                                    Clear(cur.Length);
                                    try
                                    {
                                        DirectoryInfo dir = cur[index] as DirectoryInfo;

                                        indexhist.Push(index);
                                        parent.Push(cur);
                                        index = 0;
                                        FileSystemInfo[] searchedAr = new FileSystemInfo[searchedFsis.Count];
                                        for (int i = 0; i < searchedFsis.Count; i++)
                                        {
                                            searchedAr[i] = searchedFsis[i];
                                        }
                                        cur = searchedAr;

                                    }
                                    catch (Exception e) { }
                                }
                                searchedFsis.Clear();
                                ClearMkPart();
                            }
                            else
                            {
                                CreateFolderFile(parent.First()[indexhist.First()] as DirectoryInfo);
                                cur = (parent.First()[indexhist.First()] as DirectoryInfo).GetFileSystemInfos();
                                Clear(28);
                            }
                        }
                        else
                        {
                            create = Create.NoN;
                            ClearMkPart();
                        }

                        break;



                }
                if (cur != null)
                {
                    showInfo(cur, index);
                    if (show)
                    {

                        if (cur.Length > 0) ShowInfo(cur[index]);
                        else Clear();
                        if (indexhist.Count > 0)
                        {
                            ShowDirectory(parent.First()[indexhist.First()] as DirectoryInfo);
                        }
                        else
                        {
                            Console.SetCursorPosition(22, 42);
                            Console.Write(new string(' ', Console.WindowWidth - 23));
                        }
                    }
                }

                pressed = Console.ReadKey(true);

            }
        }
 
                    
            static void showInfoPattern()
            {
                for (int i = 36; i <= 43; i++)
                {
                    Console.SetCursorPosition(1, i);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(new string(' ', Console.WindowWidth - 2));
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(3, 36);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("NAME: ");
                Console.SetCursorPosition(3, 37);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("PATH: ");
                Console.SetCursorPosition(3, 38);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("CREATED: ");
                Console.SetCursorPosition(3, 39);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("LAST MODIFIED: ");
                Console.SetCursorPosition(3, 42);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("CURRENT DIRECTORY: ");
            }
            static void ShowInfo(FileSystemInfo fsi)
            {

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(9, 36);
                Console.Write(fsi.Name.Length < 20 ? fsi.Name : fsi.Name.Substring(0, 20));
                Console.SetCursorPosition(9, 37);
                Console.Write(fsi.FullName.Length < 20 ? fsi.FullName : fsi.FullName.Substring(0, 20));
                Console.SetCursorPosition(12, 38);
                Console.Write(fsi.CreationTime);
                Console.SetCursorPosition(18, 39);
                Console.Write(fsi.LastAccessTime);
            }



            static void ShowHelp()
            {
                for (int i = 36; i <= 43; i++)
                {
                    Console.SetCursorPosition(1, i);
                    Console.Write(new string(' ', Console.WindowWidth - 2));
                }
                StreamReader sr = new StreamReader("help.txt");
                Console.SetCursorPosition(2, 36);
                Console.ForegroundColor = ConsoleColor.Yellow;
                for (int i = 36; i <= 43; i++)
                {
                    Console.SetCursorPosition(2, i);
                    Console.Write(sr.ReadLine());
                }
                sr.Close();

            }
        static void ShowDirectory(DirectoryInfo dir)
        {

            Console.SetCursorPosition(22, 42);
            Console.Write(new string(' ', Console.WindowWidth - 23));
            Console.SetCursorPosition(22, 42);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(dir.Name.Length < 20 ? dir.Name : dir.Name.Substring(0, 20));
        }


        static void Clear(int length)
        {

            for (int i = 1; i <= length; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(new string(' ', Console.WindowWidth - 2));
            }
            if (length > Console.WindowWidth) drawBorder();

        }

        static void Clear()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(9, 36);
            Console.Write(new string(' ', Console.WindowWidth - 10));
            Console.SetCursorPosition(9, 37);
            Console.Write(new string(' ', Console.WindowWidth - 10));
            Console.SetCursorPosition(12, 38);
            Console.Write(new string(' ', Console.WindowWidth - 13));
            Console.SetCursorPosition(18, 39);
            Console.Write(new string(' ', Console.WindowWidth - 20));
        }

        static void ClearMkPart()
        {
            for (int i = 30; i <= 33; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(new string(' ', Console.WindowWidth - 2));

            }

        }

        static void EnterWord()
        {
            Console.SetCursorPosition(3, 30);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            switch (create)
            {
                case Create.Folder:
                    Console.Write("Enter the name of the folder");
                    break;
                case Create.File:
                    Console.Write("Enter the name of the file");
                    break;
                case Create.Search:
                    Console.Write("Enter the name of searche file");
                    break;
            }

            Console.SetCursorPosition(9, 32);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = true;
            switch (create)
            {

                case Create.Folder:
                    folderName = Console.ReadLine();
                    break;
                case Create.File:
                    fileName = Console.ReadLine();
                    break;
                case Create.Search:
                    searchedWord = Console.ReadLine();
                    break;
            }

            Console.CursorVisible = false;
        }

        static void CreateFolderFile(DirectoryInfo dir)
        {
            switch (create)
            {
                case Create.Folder:

                    Directory.CreateDirectory(dir.FullName + "\\" + CheckName(dir, folderName, 0));
                    folderName = "New Folder";
                    break;
                case Create.File:
                    File.Create(dir.FullName + "\\" + CheckName(dir, fileName, 0));
                    fileName = "NewFile.txt";
                    break;
            }
            create = Create.NoN;
            ClearMkPart();


        }

        static string CheckName(DirectoryInfo dir, string name, int cnt)
        {
            string pattern = @"\(\d\)$";
            FileSystemInfo[] arDir = dir.GetFileSystemInfos();

            foreach (FileSystemInfo fsi in arDir)
            {
                if (fsi.Name == name)
                {
                    cnt++;
                    if (Regex.IsMatch(name, pattern))
                    {
                        if (cnt <= 9) name = name.Remove(name.Length - 3, 3);
                        else name = name.Remove(name.Length - 4, 4);
                    }
                    name += "(" + cnt + ")";

                    CheckName(dir, name, cnt);
                }
            }
            return name;
        }



        static void OpenFile(FileInfo fi)
        {
            StreamReader sr = new StreamReader(fi.FullName);
            string s = sr.ReadToEnd();
            Clear(28);
            int k = 2;
            Console.SetCursorPosition(2, k);
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < s.Length; i++)
            {
                if (k > 27) return;
                if (i % 46 == 0) Console.SetCursorPosition(2, k++);

                Console.Write(s[i]);
            }
            sr.Close();

        }

        static void Search(string s, DirectoryInfo dir)
        {
            try
            {
                FileSystemInfo[] fsi = dir.GetFileSystemInfos();
                foreach (FileSystemInfo fs in fsi)
                {
                    if (fs.Name.Contains(s)) searchedFsis.Add(fs);
                    if (fs is DirectoryInfo) Search(s, fs as DirectoryInfo);
                }
            }
            catch (Exception e) { }
        }

    }
    }

