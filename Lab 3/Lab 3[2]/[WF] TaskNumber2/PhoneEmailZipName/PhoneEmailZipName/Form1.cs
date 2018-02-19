using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneEmailZipName
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string phonePattern = @"^((\(\d{3}\))|(\d{3}))[\s\-]?\d{3}[\s\-]?\d{4}$";
            string zip = @"^(\d{4})(\-\d{4})?$";
            string email = @"^[a-z0-9_\.\-]+@[a-z]+\.[a-z]{2,6}$";
            string name = @"^[A-Z]{1}[a-z]+$";

            string ans = "";
            if (!Regex.IsMatch(textBox1.Text, name)) ans += "Name not matching\n";
            if (!Regex.IsMatch(textBox2.Text, phonePattern)) ans += "Phone not matching\n";
            if (!Regex.IsMatch(textBox3.Text, email)) ans += "Email not matching\n";
            if (!Regex.IsMatch(textBox4.Text, zip)) ans += "ZIP not matching";

            DialogResult d = new DialogResult();
            if (ans != "") d = MessageBox.Show(ans, "", MessageBoxButtons.OK);
            else { d = MessageBox.Show("All fields are correct", "", MessageBoxButtons.OK); ans = ""; }

            if (d == DialogResult.OK && ans == "")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }
    }
}
