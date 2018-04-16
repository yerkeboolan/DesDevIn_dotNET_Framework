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

namespace OOPaint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Point prev;
        Shape shape;
        Shape newShape;
        private void Form1_Load(object sender, EventArgs e)
        {        }

        bool isDrag = false; bool isRes = false;
        Point clicked = Point.Empty;
        public void _MouseMove(object sender, MouseEventArgs e)
        {
            Control cur = (Control)sender;
            
            if (isDrag)
            {
                cur.Left = e.X - clicked.X + cur.Left;
                cur.Top = e.Y - clicked.Y + cur.Top;
                cur.BringToFront();
                cur.BackColor = Color.Green;
            }
            else if (isRes)
            {
                
                if (cur.Cursor == Cursors.SizeNWSE)
                {
                    cur.Width = e.X;
                    cur.Height = e.Y;
                    
                }
                else if(cur.Cursor == Cursors.SizeWE)
                {
                    cur.Width = e.X;
                }
                else if (cur.Cursor == Cursors.SizeNS) {
                    cur.Height = e.Y;
                }
                
            }
            else
            {
                if ((((e.X + 7) > cur.Width) && ((e.Y + 7) > cur.Height)) || (((e.X - 7) < 0) && ((e.Y - 7) < 0)))
                {
                    cur.Cursor = Cursors.SizeNWSE;
                }
                else if ((((e.X + 7) > cur.Width) && ((e.Y - 7) < 0)) || (((e.X - 7) < 0) && ((e.Y + 7) > cur.Height)))
                {
                    cur.Cursor = Cursors.SizeNESW;
                }
                else if((e.X+7) > cur.Width || (e.X - 7) < 0)
                {
                    cur.Cursor = Cursors.SizeWE;
                }
                else if((e.Y + 7) > cur.Height || (e.Y - 7) < 0)
                {
                    cur.Cursor = Cursors.SizeNS;
                }
                else
                {
                    cur.Cursor = Cursors.SizeAll;
                }
            }
        }
        public void DrawNWSE(Control cur, Point e) {
            cur.Width = e.X;
            cur.Height = e.Y;
        }
        void _MouseDown(object sender, MouseEventArgs e)
        {
            Control cur = (Control)sender;
            clicked = e.Location;
            cur.BringToFront();
            if ((e.X + 5) > cur.Width || (e.Y + 5) > cur.Height)
            {
                isRes = true;
            }
            else isDrag = true;
        }
        private void _MouseUp(object sender, MouseEventArgs e)
        {
            Control cur = (Control)sender;
            cur.BackColor = Color.Green;
            isDrag = false; isRes = false;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //shapik(Shape.ShapeType.Rectangle);
        }
        void shapik(Shape.ShapeType st, Point cur)
        {
            newShape = new Shape();
            newShape.Size = new Size(0, 0);
            newShape.ForeColor = Color.Black;
            newShape.Type = st;
            newShape.Location = cur;
            newShape.MouseDown += new MouseEventHandler(_MouseDown);
            newShape.MouseMove += new MouseEventHandler(_MouseMove);
            newShape.MouseUp += new MouseEventHandler(_MouseUp);
            newShape.BackColor = Color.Green;
           // shape = newShape;
           this.Controls.Add(newShape);
            // MessageBox.Show("sssssssssssssssssssssssssssss");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Text)
            {
                case "Rectangle":
                    Shape.shape = Shape.ShapeType.Rectangle;
                    break;
                case "Circle":
                    Shape.shape = Shape.ShapeType.Circle;
                    break;
                case "Triangle":
                    Shape.shape = Shape.ShapeType.Triangle;
                    break;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            // Shape.prev = e.Location;
            prev = e.Location;
            //MessageBox.Show("fasdfasfaef");
            
            switch (Shape.shape)
            {
                case Shape.ShapeType.Rectangle:
                    shapik(Shape.ShapeType.Rectangle, e.Location);
                    break;
                case Shape.ShapeType.Circle:
                    shapik(Shape.ShapeType.Circle,  e.Location);
                    break;
                case Shape.ShapeType.Triangle:
                    shapik(Shape.ShapeType.Triangle,  e.Location);
                    break;
            }


        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left)
            {
                if (!isDrag) {
                    
                    switch (Shape.shape)
                    {
                        case Shape.ShapeType.Rectangle:
                            Draw(e.Location);
                            break;
                        case Shape.ShapeType.Circle:
                            Draw(e.Location);
                            break;
                        case Shape.ShapeType.Triangle:
                            Draw(e.Location);
                            break;

                    }

                }
            }
            
        }

        public void Draw(Point cur) {
            if (cur.X >= prev.X && cur.Y >= prev.Y)
            {
                Cursor = Cursors.SizeNWSE;
                newShape.Width = cur.X - prev.X;
                newShape.Height = cur.Y - prev.Y;
            }
            else if (cur.X >= prev.X && cur.Y <= prev.Y)
            {
                Cursor = Cursors.SizeNESW;
                newShape.Top = cur.Y;
                newShape.Width = cur.X - prev.X;
                newShape.Height = prev.Y - cur.Y;
            }
            else if (cur.X <= prev.X && cur.Y >= prev.Y) {
                Cursor = Cursors.SizeNESW;
                newShape.Left = cur.X;
                newShape.Width = prev.X- cur.X;
                newShape.Height = cur.Y - prev.Y; ;
            }
            else
            {
                Cursor = Cursors.SizeNWSE;
                newShape.Top = cur.Y;
                newShape.Left = cur.X;
                newShape.Width = prev.X - cur.X;
                newShape.Height = prev.Y - cur.Y;
            }
        }

        
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {

            Cursor = Cursors.Default;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
               // e.Graphics.DrawPath(pen, path);
        }
    }
    public class Shape : UserControl
    {
        public static ShapeType shape = ShapeType.Rectangle;
        private GraphicsPath path;
        public enum ShapeType { Rectangle, Circle, Triangle }
        public static Point prev;
        public void make()
        {
           path = new GraphicsPath();
            switch (shape)
            {
                case ShapeType.Rectangle:
                    path.AddRectangle(this.ClientRectangle);
                    break;
                case ShapeType.Circle:
                    path.AddEllipse(this.ClientRectangle);
                    break;
                case ShapeType.Triangle:
                    Point[] p =
                    {
                         new Point(ClientRectangle.Left+ClientRectangle.Width, ClientRectangle.Top + ClientRectangle.Height),
                        new Point(ClientRectangle.Left, ClientRectangle.Top+ClientRectangle.Height-1),
                        new Point(ClientRectangle.Left + ClientRectangle.Width/2, ClientRectangle.Top)
                        
                       
                    };
                   
                    path.AddPolygon(p);
                  
                    break;
                   
            }
            this.Region = new Region(path);
        }

                public ShapeType Type
        {
            get { return shape; }
            set
            {
                shape = value;
                 make();
            }
        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (path != null)
            {
                e.Graphics.FillPath(new SolidBrush(this.BackColor), path);
                e.Graphics.DrawPath(new Pen(this.ForeColor, 4), path);
            }
        }
        protected override void OnResize(System.EventArgs e)
        {
            make();
            this.Invalidate();//question 1
        }
    }
}
