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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BooLean_Load(object sender, EventArgs e)
        {
            label1.Text = this.Name;
            timer1.Start();
            timer1.Interval = 1000;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum) progressBar1.Value++;
            else
            {
                timer1.Stop();
            }
            seconds++;
            if (seconds > 59) {
                minutes++;
                seconds = 0;
            }
            if(minutes > 59)
            {
                hours++;
                minutes = 0;
            }

            TimeSpan ts = new TimeSpan(hours, minutes, seconds);
            label2.Text = ts.ToString();

        }
        
        //(Form.ActiveForm.Controls["numericUpDown1"] as NumericUpDown).Value;
    }
}
