using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskTimer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BooLean b = new BooLean();
             if(textBox1.Text != "")
             {
                
                b.Name = textBox1.Text;
                flowLayoutPanel1.Controls.Add(b);
            }
            else
            {
                
                b.Name = textBox1.Text;
                b.Name = "Task";
                flowLayoutPanel1.Controls.Add(b);
                
            }
            textBox1.Clear();
            textBox1.Text = "Task Name";
            textBox1.ForeColor = Color.Silver;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(textBox1.Text == "Task Name") {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

       
    }
}
