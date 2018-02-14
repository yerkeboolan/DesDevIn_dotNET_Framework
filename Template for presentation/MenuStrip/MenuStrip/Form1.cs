using System;
using System.Drawing;
using System.Windows.Forms;

namespace MenuStrip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            menuStrip1.Dock = DockStyle.Top;

            menuStrip1.Hide();

        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ImeModeChanged(object sender, EventArgs e)
        {

        }

        /*private void menuStrip1_MenuDeactivate(object sender, EventArgs e)
        {
            MessageBox.Show("Menu Bar on deactivating mode");
        }*/


        
           

    }
}
