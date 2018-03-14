using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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
        int h = 0;
        int m = 0;
        int s = 0;

        private void BooLean_Load(object sender, EventArgs e)
        {

            label1.Text = this.Name;

            timer1.Start();
            timer1.Interval = 1000;


            if ((Form.ActiveForm.Controls["checkBox1"] as CheckBox).Checked == false)
            {
                timer1.Stop();
                startNstop.BackgroundImage = (System.Drawing.Image)(Properties.Resources.Start_icon);
            }
            else
            {
                timer1.Start();
                startNstop.BackgroundImage = (System.Drawing.Image)(Properties.Resources.Button_2_pause_icon);
            }

            if ((Form.ActiveForm.Controls["checkBox2"] as CheckBox).Checked == true)
            {
                label2.Text = "Indefinite";
                progressBar1.Style = ProgressBarStyle.Marquee;
                if (h == 0 && m == 0)
                {
                    h = 10000;
                    m = 10000;
                }

            }
            else if ((Form.ActiveForm.Controls["checkBox2"] as CheckBox).Checked == false)
            {
                h = (int)(Form.ActiveForm.Controls["numericUpDown1"] as NumericUpDown).Value;
                m = (int)(Form.ActiveForm.Controls["numericUpDown2"] as NumericUpDown).Value;

                TimeSpan ts = new TimeSpan(h, m, s);
                label2.Text = ts.ToString();
            }

           // (Form.ActiveForm.Controls["chart1"] as Chart).Series[0].Points.AddXY(this.Name, progressBar1.Maximum);

        }

        public string getName()
        {
            return label1.Text;
        }

        public int getTime()
        {
            return seconds;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (h == 0 && m == 0)
            {
                timer1.Enabled = false;
            }
            else
            {
                progressBar1.Maximum = (h * 60 + m) * 60;
                if (progressBar1.Value < progressBar1.Maximum) progressBar1.Value++;
                else
                {
                    timer1.Stop();
                }
                seconds++;
                if (seconds > 60)
                {
                    minutes++;
                    seconds = 0;
                }
                if (minutes > 59)
                {
                    hours++;
                    minutes = 0;
                }

                TimeSpan ts = new TimeSpan(hours, minutes, seconds);
                label3.Text = ts.ToString();
            }


            if (progressBar1.Value == progressBar1.Maximum)
            {
                timer1.Stop();
                System.Media.SystemSounds.Beep.Play();
                MessageBox.Show("Task has reached the goal", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }



        private void startNstop_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                startNstop.BackgroundImage = (System.Drawing.Image)(Properties.Resources.Start_icon);
            }
            else
            {
                timer1.Start();
                startNstop.BackgroundImage = (System.Drawing.Image)(Properties.Resources.Button_2_pause_icon);
            }

        }

        private void rst_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (MessageBox.Show("Are you sure you want to reset the " + label1.Text +  " task? The task history will not be reset", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                progressBar1.Value = 0;
                hours = 0;
                minutes = 0;
                seconds = 0;
                timer1.Start();
            }
            else
            {
                timer1.Start();
            }
        }

        private void dlt_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (MessageBox.Show("Are you sure you want to delete the " + label1.Text + " task? The history of the task will be deleted.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Dispose();
            }
            else
            {
                timer1.Start();
            }
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void info_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show("Task Name:  " + label1.Text + "\n" + "Goal Time:  " + label2.Text + "\n" + "Spent Time:  " + label3.Text);
            timer1.Start();
        }
    }
}
