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
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            BooLean b = new BooLean();
             if(textBox1.Text != "")
             {
                b.Name = textBox1.Text;
                
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

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
            }
            else
            {
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            foreach (BooLean element in flowLayoutPanel1.Controls)
            {
                chart1.Series["Series1"].Points.AddXY(element.getName(), element.getTime());
            }
        }
    }
}
