using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSearcher
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The search
        /// </summary>
        string search = "";
        /// <summary>
        /// The files
        /// </summary>
        List<String> files = new List<String>();


        /// <summary>
        /// Handles the Load event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                search = textBox1.Text;
                DirectoryInfo dir = new DirectoryInfo(@"C:\Users\Yerkebulan\Desktop\Design&Dev. in .NET");
                fileSearcher(dir);
                listBox1.Items.Clear();
               
                for(int i = 0; i < files.Count; i++)
                {
                    listBox1.Items.Add(files[i]);     
                }
                files.Clear();
            }
        }

        /// <summary>
        /// Files the searcher.
        /// </summary>
        /// <param name="dir">The dir.</param>
        public void fileSearcher(DirectoryInfo dir)
        {
            FileSystemInfo[] fsi = dir.GetFileSystemInfos();
            foreach(FileSystemInfo f in fsi)
            {
                if(f is FileInfo)
                {
                    if(f.Name.Contains(search))
                    {
                        files.Add(f.FullName);
                    }
                        
                        
                }
                else if(f is DirectoryInfo)
                {
                    fileSearcher(f as DirectoryInfo);
                }
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the listBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(listBox1.SelectedItem.ToString());
        }
    }
}
