using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Xml;

namespace DubaiMall2
{

    public partial class Form1 : Form
    {
        /// <summary>
        /// The main class with all shops in Dubai Mall
        /// </summary>
        List<JsonClass> info = new List<JsonClass>();
        /// <summary>
        /// Shops divided by levels
        /// </summary>
        List<JsonClass> levelShops = new List<JsonClass>();
        /// <summary>
        /// List of categories in Dubai Mall
        /// </summary>
        SortedSet<Category> categories = new SortedSet<Category>(); 
        enum Cases { Floors, Categories, Shops, ShopInfo};
        Cases currentCase = Cases.Floors;
        /// <summary>
        /// Set of shops which user choose to draw a path
        /// </summary>
        SortedSet<int> checkedShops = new SortedSet<int>();
        /// <summary>
        /// Floor which user choosed
        /// </summary>
        int selectedIndex;
        int floorId;
        /// <summary>
        /// Shop, about which user want to know more information
        /// </summary>
        string currentShop = "";
        Category categ = new Category();
        /// <summary>
        /// Indexes of previous step which user made
        /// </summary>
        Stack<int> parentIndex = new Stack<int>();

        /// <summary>
        /// Bitmap in which program drawing path
        /// </summary>
        private Bitmap map;
        private Dictionary<Tuple<Point, Point>, List<Point>> path = new Dictionary<Tuple<Point, Point>, List<Point>>();
        private Dictionary<Tuple<Point, Point>, int> d = new Dictionary<Tuple<Point, Point>, int>();

        private List<Point> points = new List<Point>();
        /// <summary>
        /// All points which we have
        /// </summary>
        private List<Point> allPoints = new List<Point>();
        /// <summary>
        /// Adjacency matrix
        /// </summary>
        private int[,] matrix;
        /// <summary>
        /// Checking sides
        /// </summary>
        int[] dx = { 1, -1, 0, 0, 1, 1, -1, -1 };
        int[] dy = { 0, 0, 1, -1, -1, 1, 1, -1 };
        /// <summary>
        /// Array for dynamic run through
        /// </summary>
        int[,] dp;
        /// <summary>
        /// Initializing components
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            LoadJson();
            /*for (int i = 0; i < info.Count; i++) {
                nameKeys.Add(info[i].name, i );
            }
            */
            foreach(JsonClass jc in info)
            {
                foreach (Category cat in jc.categories) categories.Add(cat);
                
            }
            FillFloors();
            button1.Visible = false;
            button2.Visible = true;
            button3.Visible = false;
            listBox1.Visible = false;
            textBox1.Visible = false;
            label1.Visible = false;
            checkedListBox1.Visible = false;
            checkedListBox2.Visible = false;
            button4.Visible = false;
        }
        /// <summary>
        /// Initializing components
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            setPoints();
            //readPath();
        }
        /// <summary>
        /// Equate locations to shops
        /// </summary>
        public void setPoints() {
            StreamReader sr = new StreamReader(@"input.txt");
            string s = "";
            int k = 0;
            for (int i = 0; i < info.Count; i++)
            {
                
                    s = sr.ReadLine();
                    if (s == null)
                    {
                        // System.Diagnostics.Debug.WriteLine(i);
                        sr = new StreamReader(@"input.txt");
                        continue;
                    }
                    // System.Diagnostics.Debug.WriteLine(i);
                    if (s == "") continue;
                    String[] token = s.Split(' ');
                    Point location = new Point(Int32.Parse(token[3]), Int32.Parse(token[2]));

                    info[i].P = location;
                //    levelShops.Add(info[i]);
                    k++;
                    //listBox2.Items.Add(i + " " + info[i].P);
                
            }

        }

        List<Point> desListNumbers;
        /// <summary>
        /// Reading JSON file, creating and filling classes
        /// </summary>
        public void LoadJson()
        {
            using (StreamReader r = new StreamReader("info.json"))
            {
                string json = r.ReadToEnd();
                info = JsonConvert.DeserializeObject<List<JsonClass>>(json);
            }
        }
        
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            int index = listBox1.SelectedIndex + 1;
            
            parentIndex.Push(index - 1);
            switch (currentCase)
            {
                case Cases.Floors:
                    floorId = index;
                    listBox1.Items.Clear();
                    FillCategories();
                    currentCase = Cases.Categories;
                    label2.Text = "Floor: " +floorId.ToString();
                    break;
                case Cases.Categories:
                    //categoryName = listBox1.Items[index].ToString();
                    categ = categories.ElementAt(index - 1);
                    listBox1.Items.Clear();
                    checkedListBox1.Items.Clear();
                    listBox1.Visible = false;
                    checkedListBox1.Visible = true;
                    FillShops();
                    currentCase = Cases.Shops;
                    label3.Text = categ.name;
                    break;
                case Cases.ShopInfo:
                    
                    OpenVideo();
                    break;
            }
        }
        /// <summary>
        /// Function for find index of this shop in list "info"
        /// </summary>
        /// <param name="s">Text of item in listBox or checkedListBox</param>
        /// <returns></returns>
        public int FindIndex(string s)
        {
            string ans = "";
            for (int i = 0; i < s.Length; i++) {
                if (Char.IsDigit(s[i])) ans += s[i];
                else break;
            }
            System.Diagnostics.Debug.WriteLine(ans);
            return Int32.Parse(ans)-1;
        }
        /// <summary>
        /// Filling listBox floor case whith floor ids
        /// </summary>
        public void FillFloors() {
            label1.Text = "Floors";
            for (int i = 1; i <= 16; i++)
            {
                listBox1.Items.Add("---" + i);
            }
        }
        /// <summary>
        /// Filling listBox categories case with categories
        /// </summary>
        public void FillCategories()
        {
            label1.Text = "Categories";

            for (int i = 0; i<categories.Count; i++)
            {
                listBox1.Items.Add(i + 1+" " + categories.ElementAt(i).name);
            }
        }
        /// <summary>
        /// Filling checkListBox shops case with shops
        /// </summary>
        public void FillShops()
        {
            label1.Text = "Shops";
            int k = 0;
            for (int i = 0;i < info.Count; i++)
            {
                if (floorId == info[i].floorId)
                {
                    foreach(Category cat in info[i].categories) {
                        if (cat.name == categ.name)
                        {
                            checkedListBox1.Items.Add(i + 1 + " " + info[i].name);
                            
                            for (int j = 0; j < checkedShops.Count; j++)
                            {
                                
                                if (checkedShops.ElementAt(j) == i)
                                    {
                                    System.Diagnostics.Debug.WriteLine(checkedShops.ElementAt(j));
                                    checkedListBox1.SetItemChecked(k, true);
                                        

                                    }
                               
                            }
                            k++;
                        }
                    }
                }
                
               
            }
            
        }
        /// <summary>
        /// Fill info about shop whith specified index
        /// </summary>
        /// <param name="index">Index in list "info"</param>
        public void FillShopInfos(int index) {
            label1.Text = "Shop Info";


            listBox1.Items.Clear();
                listBox1.Items.Add("Name: " + info[index].name );
                listBox1.Items.Add("Email: " + info[index].email);
                listBox1.Items.Add("Phone: " + info[index].phone);
                listBox1.Items.Add("Card: " + info[index].giftCard);
                listBox1.Items.Add("FloorId: " + info[index].floorId);
                listBox1.Items.Add("Level: " + info[index].level);
                listBox1.Items.Add("Description: " + info[index].description);
                listBox1.Items.Add("Update Date: " + info[index].updateDate);
                listBox1.Items.Add("Tags: ");
            string s = info[index].video.link;
                listBox1.Items.Add("Video: " + s);
                for (int i = 0; i < info[index].tags.Count(); i++)
                {
                    int k = i + 1;
                    listBox1.Items.Add("-------  " + k + " " + info[index].tags[i].name);
                }
            
            

        }
        /// <summary>
        /// Function to open video
        /// </summary>
        public void OpenVideo() {
            
            if (listBox1.Items[parentIndex.First()].ToString().Contains("Video"))
            {
                parentIndex.Pop();
                int at = FindIndex(currentShop);
                if (info[at].video.link.ToString() != null)
                    System.Diagnostics.Process.Start(info[at].video.link.ToString());
            }
            
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text != "")
            {
                textBox1.Text = "";
                //checkedListBox2.Visible = false;

            }
            else
            {
                if (parentIndex.Count <= 0) parentIndex.Push(1);
                if (currentCase != Cases.Floors) listBox1.Items.Clear();
                switch (currentCase)
                {
                    case Cases.Floors:
                        break;
                    case Cases.Categories:
                        FillFloors();
                        currentCase = Cases.Floors;
                        checkedShops.Clear();
                        listBox1.SelectedIndex = parentIndex.First();
                        label2.Text = "";
                        break;
                    case Cases.Shops:
                        listBox1.Visible = true;
                        checkedListBox1.Visible = false;

                        FillCategories();
                        currentCase = Cases.Categories;
                        listBox1.SelectedIndex = parentIndex.First();
                        label3.Text = "";
                        break;
                    case Cases.ShopInfo:
                        //checkedListBox1.Items.Clear();
                        //FillShops();
                        checkedListBox1.Visible = true;
                        listBox1.Visible = false;
                        currentCase = Cases.Shops;
                        checkedListBox1.SelectedIndex = parentIndex.First();
                        break;
                }
                if (parentIndex.Count > 0) parentIndex.Pop();
            }
           


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "")
            {
                checkedListBox2.Visible = false;
                checkedListBox2.Items.Clear();
                if (currentCase == Cases.Shops) checkedListBox1.Visible = true;
                else listBox1.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button4.Visible = true;
            button2.Visible = false;
            button1.Visible = true;
            button3.Visible = true;
            listBox1.Visible = true;
            textBox1.Visible = true;
            label1.Visible = true;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void checkedListBox1_DoubleClick(object sender, EventArgs e)
        {
            int index = checkedListBox1.SelectedIndex + 1;
            currentShop = checkedListBox1.Items[index - 1].ToString();
            parentIndex.Push(index - 1);
            switch (currentCase)
            {
                case Cases.Shops:
                    int at = FindIndex(checkedListBox1.Items[index-1].ToString());
                    listBox1.Items.Clear();
                    //System.Diagnostics.Debug.WriteLine(listBox1.Items[1]);
                    listBox1.Visible = true;
                    checkedListBox1.Visible = false;
                    FillShopInfos(at);
                    currentCase = Cases.ShopInfo;
                    
                    break;
                
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Clear();
            listBox1.Visible = false;
            checkedListBox1.Visible = false;
            checkedListBox2.Visible = true;
            foreach (int i in checkedShops) {
                System.Diagnostics.Debug.WriteLine(i);
            }
            currentCase = Cases.Shops;
            int k = 0;
            for (int i = 0; i < info.Count; i++)
            {
                if (info[i].name.ToUpper().Contains(textBox1.Text) || info[i].name.ToLower().Contains(textBox1.Text))
                {
                    checkedListBox2.Items.Add(i + 1 + " " + info[i].name);
                    for (int j = 0; j < checkedShops.Count; j++)
                    {
                        if (checkedShops.ElementAt(j) == i)
                        {
                            System.Diagnostics.Debug.WriteLine(checkedShops.ElementAt(j));
                            checkedListBox2.SetItemChecked(k, true);
                        }
                    }
                    k++;

                }
            }
            //listBox1.SelectedItem = listBox1.Items[0];
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                //System.Diagnostics.Debug.WriteLine(checkedListBox1.Items[e.Index].ToString());
                checkedShops.Add(FindIndex(checkedListBox1.Items[e.Index].ToString()));
            }
            else if(e.NewValue == CheckState.Unchecked)
            {
                bool ok = checkedShops.Remove(FindIndex(checkedListBox1.Items[e.Index].ToString()));
                
                System.Diagnostics.Debug.WriteLine(ok);
            }
        }
        /// <summary>
        /// In CCities class We take all given points to class by location
        /// </summary>
        class CCities
        {
            public Point[] Coordinate;
            public CCities(int N, int maxValue)
            {
                Random random = new Random();

                Coordinate = new Point[N];

                int minBorder = (int)(maxValue * 0.05);
                int maxBorder = (int)(maxValue * 0.95);

                for (int i = 0; i < N; i++)
                {
                    Coordinate[i] = new Point(random.Next(minBorder, maxBorder),
                                              random.Next(minBorder, maxBorder));
                }
            }
        }
        /// <summary>
        /// CPath class take locations from CCities and calculate distance between cities 
        /// </summary>
        class CPath
        {
            double[,] distance;
            public int[] Path;
            public CPath(CCities map)
            {
                distance = new double[map.Coordinate.Length, map.Coordinate.Length];
                for (int j = 0; j < map.Coordinate.Length; j++)
                {
                    distance[j, j] = 0;
                    for (int i = 0; i < map.Coordinate.Length; i++)
                    {
                        double value = Math.Sqrt(Math.Pow(map.Coordinate[i].X - map.Coordinate[j].X, 2) +
                                                 Math.Pow(map.Coordinate[i].Y - map.Coordinate[j].Y, 2));
                        distance[i, j] = distance[j, i] = value;
                    }
                }
                Path = new int[map.Coordinate.Length + 1];
                for (int i = 0; i < map.Coordinate.Length; i++)
                {
                    Path[i] = i;
                }
                Path[map.Coordinate.Length] = 0;
            }
            /// <summary>
            /// In FindBestPath we solve algorithm "Имитация отжига комиваяжоры", We took random locations and save Best Path, how function work more results better.
            /// </summary>
            public void FindBestPath()
            {
                Random random = new Random();
                for (int fails = 0, F = Path.Length * Path.Length; fails < F;)
                {
                    int p1 = 0, p2 = 0;
                    while (p1 == p2)
                    {
                        p1 = random.Next(1, Path.Length - 1);
                        p2 = random.Next(1, Path.Length - 1);
                    }
                    double sum1 = distance[Path[p1 - 1], Path[p1]] + distance[Path[p1], Path[p1 + 1]] +
                                  distance[Path[p2 - 1], Path[p2]] + distance[Path[p2], Path[p2 + 1]];
                    double sum2 = distance[Path[p1 - 1], Path[p2]] + distance[Path[p2], Path[p1 + 1]] +
                                  distance[Path[p2 - 1], Path[p1]] + distance[Path[p1], Path[p2 + 1]];
                    if (sum2 < sum1)
                    {
                        int temp = Path[p1];
                        Path[p1] = Path[p2];
                        Path[p2] = temp;
                    }
                    else
                    {
                        fails++;
                    }
                }
            }
            /// <summary>
            /// PathLength calculate sum of all distance
            /// </summary>
            /// <returns></returns>
            public double PathLength()
            {
                double pathSum = 0;
                for (int i = 0; i < Path.Length - 1; i++)
                {
                    pathSum += distance[Path[i], Path[i + 1]];
                }
                return pathSum;
            }
        }
        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                checkedShops.Add(FindIndex(checkedListBox2.Items[e.Index].ToString()));
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                bool ok = checkedShops.Remove(FindIndex(checkedListBox2.Items[e.Index].ToString()));
            }
        }

        private void checkedListBox2_DoubleClick(object sender, EventArgs e)
        {
            int index = checkedListBox2.SelectedIndex + 1;
            currentShop = checkedListBox2.Items[index - 1].ToString();
            parentIndex.Push(index-1);
            switch (currentCase)
            {
                case Cases.Shops:
                    int at = FindIndex(checkedListBox2.Items[index - 1].ToString());
                    listBox1.Items.Clear();
                    //System.Diagnostics.Debug.WriteLine(listBox1.Items[1]);
                    listBox1.Visible = true;
                    checkedListBox2.Visible = false;
                    FillShopInfos(at);
                    currentCase = Cases.ShopInfo;
                    break;
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (checkedShops.Count > 0)
            {
                d.Clear();
                readMap();
                findPath();
            }
        }
        /// <summary>
        /// Reads map from image
        /// </summary>
        public void readMap()
        {
            string[] lines = File.ReadAllLines(@"matrix.txt");
            map = (Bitmap)Image.FromFile(@"dubai-mall-level-g.png");
            matrix = new int[map.Width, map.Height];
            dp = new int[map.Width, map.Height];
            for (int i = 0; i < map.Width; ++i)
            {
                string[] token = lines[i].Split(' ');
                for (int j = 0; j < map.Height; ++j)
                    matrix[i, j] = Convert.ToInt32(token[j]);
            }
        }
        /// <summary>
        /// We calculate all min paths in one document and take this from file. And this work O(1)
        /// </summary>
        public void readPath()
        {
            foreach (Point p1 in allPoints)
            {
                foreach (Point p2 in allPoints)
                {
                    if (p1 != p2)
                        d.Add(new Tuple<Point, Point>(p1, p2), 0);
                }
            }
            using (StreamWriter file2 = new StreamWriter(@"temp.txt"))
            {
                foreach (Point p1 in allPoints)
                    foreach (Point p2 in allPoints)
                    {
                        file2.Write(p1.X + " " + p1.Y + " ");
                        file2.Write(p2.X + " " + p2.Y + " ");
                        if (p1 != p2)
                            file2.WriteLine(d[new Tuple<Point, Point>(p1, p2)]);
                    }
            }
            var watch = System.Diagnostics.Stopwatch.StartNew();
            StreamReader fs = new StreamReader(@"minPath.txt");
            string s = "";
            while (true)
            {
                s = fs.ReadLine();
                if (s == null) break;
                if (s == "") continue;
                String[] token = s.Split(' ');
                Point point1 = new Point(Int32.Parse(token[1]), Int32.Parse(token[0]));
                Point point2 = new Point(Int32.Parse(token[3]), Int32.Parse(token[2]));
                int length = Int32.Parse(token[4]);
                if (!d.ContainsKey(new Tuple<Point, Point>(point1, point2)))
                    d[new Tuple<Point, Point>(point1, point2)] = length;
            }

            StreamReader fs2 = new StreamReader(@"Path.txt");
            s = "";
            int i = 0, j = 0;
            while (true)
            {
                s = fs2.ReadLine();
                if (s == null) break;
                if (s == "") continue;
                String[] token = s.Split(' ');
                System.Diagnostics.Debug.WriteLine(path.Count);
                    if (!path.ContainsKey(new Tuple<Point, Point>(allPoints[i], allPoints[j])))
                        path.Add(new Tuple<Point, Point>(allPoints[i], allPoints[j]), new List<Point>());

                for (int k = 0; k < token.Length - 1; k += 2)
                    path[new Tuple<Point, Point>(allPoints[i], allPoints[j])].Add
                        (new Point(Int32.Parse(token[k + 1]), Int32.Parse(token[k])));

                j++;
                if (j == allPoints.Count) { i++; j = 0; }
                if (i == allPoints.Count) i = 0;
            }
            watch.Stop();
            System.Diagnostics.Debug.WriteLine(watch.ElapsedMilliseconds);
        }
        /// <summary>
        /// Finds path from enter to all needed shops
        /// </summary>
        public void findPath()
        {
            Point enter = new Point(471, 317);
            List<Point> points = new List<Point>();
            foreach (int i in checkedShops)
            {
                for (int k = 0; k < 8; ++k)
                {
                    try
                    {
                        map.SetPixel(info[i].P.X + dx[k], info[i].P.Y + dy[k], Color.Black);
                    }
                    catch
                    {
                        System.Diagnostics.Debug.WriteLine(info[i].P.X + " " + dx[k] + " " + info[i].P.Y + " "  + dy[k]);
                    }
                }
            }
            points.Add(enter);
            foreach (int i in checkedShops)
                points.Add(info[i].P);
            Dictionary<Point, int> used = new Dictionary<Point, int>();
            for (int i = 0; i < points.Count; ++i)
            {
                findMinPath(points[i]);
                for (int j = 0; j < points.Count; ++j)
                {
                    if (!d.ContainsKey(new Tuple<Point, Point>(points[i], points[j])))
                    { 
                    d.Add(new Tuple<Point, Point>(points[i], points[j]), dp[points[j].X, points[j].Y]);
                    DrawPath(points[i], points[j]);
                    }
                }
            }
            for (int i = 0; i < points.Count; ++i)
                if(!used.ContainsKey(points[i]))
                used.Add(points[i], 0);
            Point cur = points[0];
            while (true)
            {
                int MIN = 1000000;
                Point next = Point.Empty;
                used[cur] = 1;
                foreach (Point p in points)
                {
                    if (d.ContainsKey(new Tuple<Point, Point>(cur, p)))
                    {
                        System.Diagnostics.Debug.WriteLine(d[new Tuple<Point, Point>(cur, p)]);
                        if (used[p] == 0 && MIN > d[new Tuple<Point, Point>(cur, p)] && d[new Tuple<Point, Point>(cur, p)] > 0)
                        {
                            MIN = d[new Tuple<Point, Point>(cur, p)];
                            System.Diagnostics.Debug.WriteLine(d[new Tuple<Point, Point>(cur, p)]);
                            next = p;
                        }
                    }
                }
                if (next == Point.Empty) break;
                else
                {
                    //System.Diagnostics.Debug.WriteLine(path[new Tuple<Point, Point>(cur, next)].Count);
                    foreach (Point point in path[new Tuple<Point, Point>(cur, next)])
                    {
                        for (int k = 0; k < 8; ++k)
                            map.SetPixel(point.X + dx[k], point.Y + dy[k], Color.Red);
                    }
                    cur = next;
                }
            }
            try
            {
                foreach (Point point in path[new Tuple<Point, Point>(cur, enter)])
                {
                    for (int k = 0; k < 8; ++k)
                        map.SetPixel(point.X + dx[k], point.Y + dy[k], Color.Red);
                }
            }
            catch
            {
                MessageBox.Show("Show");
            }
            foreach (int q in checkedShops)
            {
                for (int i = 0; i < 8; ++i)
                    for (int j = 0; j < 8; ++j)
                        for (int ii = 0; ii < 8; ++ii)
                            for (int jj = 0; jj < 8; ++jj)
                                map.SetPixel(info[q].P.X + dx[i] + dx[j] + dx[ii] + dx[jj],
                                             info[q].P.Y + dy[i] + dy[j] + dx[ii] + dy[jj], Color.Black);
            }
            map.Save(@"C:\Users\Yerkebulan\Desktop\Map.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            System.Diagnostics.Process.Start(@"C:\Users\Yerkebulan\Desktop\Map.bmp");
        }
        /// <summary>
        /// Counts minimal path from starting till finishing
        /// </summary>
        /// <param name="s">Starting shop</param>
        /// <param name="f">Finishing shop</param>
        /// <returns>Minimal path</returns>
        private void findMinPath(Point s)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int[,] used = new int[map.Width, map.Height];
            Queue<Point> q = new Queue<Point>();

            for (int i = 0; i < map.Width; ++i)
                for (int j = 0; j < map.Height; ++j)
                    dp[i, j] = 1000000000;

            dp[s.X, s.Y] = 0;
            q.Enqueue(s);
            while (q.Count != 0)
            {
                Point cur = q.Dequeue();
                for (int k = 0; k < 8; ++k)
                {
                    Point newPoint = new Point(cur.X + dx[k], cur.Y + dy[k]);
                    if (used[newPoint.X, newPoint.Y] == 0)
                        if (0 <= newPoint.X && newPoint.X < map.Width &&
                            0 <= newPoint.Y && newPoint.Y < map.Height &&
                            matrix[newPoint.X, newPoint.Y] == 1)
                        {
                            q.Enqueue(newPoint);
                            dp[newPoint.X, newPoint.Y] = Math.Min(dp[newPoint.X, newPoint.Y], dp[cur.X, cur.Y] + 1);
                        }
                    used[newPoint.X, newPoint.Y] = 1;
                }
            }
            watch.Stop();
            System.Diagnostics.Debug.WriteLine(watch.ElapsedMilliseconds);
        }
        /// <summary>
        /// Drawing path between two point
        /// </summary>
        /// <param name="s">Start point</param>
        /// <param name="f">Finish point</param>
        private void DrawPath(Point s, Point f)
        {
            if (!path.ContainsKey(new Tuple<Point, Point>(s, f)))
            {
                path.Add(new Tuple<Point, Point>(s, f), new List<Point>());
                int x = f.X, y = f.Y;
                while (true)
                {
                    if (x == s.X && y == s.Y) break;
                    for (int k = 0; k < 8; ++k)
                        if (0 <= x + dx[k] && x + dx[k] < map.Width &&
                            0 <= y + dy[k] && y + dy[k] < map.Height &&
                            matrix[x + dx[k], y + dy[k]] == 1)
                            path[new Tuple<Point, Point>(s, f)].Add(new Point(x + dx[k], y + dy[k]));
                    for (int k = 0; k < 8; ++k)
                    {
                        Point newPoint = new Point(x + dx[k], y + dy[k]);
                        if (dp[newPoint.X, newPoint.Y] == dp[x, y] - 1)
                        {
                            x = newPoint.X; y = newPoint.Y;
                            break;
                        }
                    }
                }
            }
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    public partial class Category : IComparable
    {
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Category cat = obj as Category;
            return this.name.CompareTo(cat.name);
        }
        public override bool Equals(object obj)
        {
            if (this.name == (obj as Category).name) return true;

            return false;
        }
    }
}
