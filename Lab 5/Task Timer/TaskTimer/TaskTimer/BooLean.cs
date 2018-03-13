using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskTimer
{
    public partial class BooLean : UserControl
    {
        public BooLean()
        {
            InitializeComponent();
        }

        int hours = 0;
        int minutes = 0;
        int seconds = 0;
        int h, m, s = 0;

        

        private void BooLean_Load(object sender, EventArgs e)
        {
            label1.Text = this.Name;
            startNstop.Text = "Sp";
            
            timer1.Start();
            timer1.Interval = 1000;

            h = (int)(Form.ActiveForm.Controls["numericUpDown1"] as NumericUpDown).Value;
            m = (int)(Form.ActiveForm.Controls["numericUpDown2"] as NumericUpDown).Value;

            TimeSpan ts = new TimeSpan(h, m, s);
            label2.Text = ts.ToString();

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((int)(Form.ActiveForm.Controls["numericUpDown1"] as NumericUpDown).Value == 0 &&
                (int)(Form.ActiveForm.Controls["numericUpDown2"] as NumericUpDown).Value == 0)
            {
                timer1.Enabled = false;
            }


            else {
                progressBar1.Maximum = ((int)(Form.ActiveForm.Controls["numericUpDown1"] as NumericUpDown).Value * 60 + (int)(Form.ActiveForm.Controls["numericUpDown2"] as NumericUpDown).Value) * 60;
                if (progressBar1.Value < progressBar1.Maximum) progressBar1.Value++;
                else
                {
                    timer1.Stop();
                }
                seconds++;
                if (seconds > 59) {
                    seconds = 0;
                    minutes++;
                }
                if (minutes > 59)
                {
                    minutes = 0;
                    hours++;
                }

                TimeSpan ts = new TimeSpan(hours, minutes, seconds);
                label3.Text = ts.ToString();
            }
        }
        private void startNstop_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                startNstop.Text = "St";
            }
            else
            {
                timer1.Start();
                startNstop.Text = "Sp";
            }

        }









        /*
        private void dlt_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            BooLean boolean = new BooLean();
            if(MessageBox.Show("Are you sure you want to delete the task? The history of the task will be deleted.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                boolean.Controls.Clear();
            }
        } */
    }
}
