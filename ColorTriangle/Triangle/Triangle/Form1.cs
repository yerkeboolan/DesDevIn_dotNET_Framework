using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triangle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int radius = 10;

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public static float GetBrightness(Color c)
        {
            return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B).GetBrightness();
        }
        List<Color> SortByBrightness(List<Color> colors)
        {
            return colors.OrderBy(GetBrightness).ToList();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int cnt = 0;
            System.Array colorsArray = Enum.GetValues(typeof(KnownColor));
            List<Color> c = new List<Color>();
            for (int i = 0; i < colorsArray.Length; ++i)
            {
                c.Add(Color.FromKnownColor((KnownColor)colorsArray.GetValue(i)));
            }
            c = SortByBrightness(c);
            c.RemoveAll(x => GetBrightness(x) < 0.50);
            int n = 15;
            for (int j = 1; j <= n; ++j)
            {
                for (int i = j; i <= n - j + 1; ++i)
                {
                    int xi = 20 + i * 10;
                    int xj = 20 + j * 10;
                    e.Graphics.FillEllipse(new SolidBrush(c.ElementAt(cnt++)), xj, xi, radius, radius);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
