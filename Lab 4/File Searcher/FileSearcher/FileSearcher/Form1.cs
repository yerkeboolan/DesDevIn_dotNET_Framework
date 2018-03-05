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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string search= "";
        List<String> files = new List<String>();


        private void Form1_Load(object sender, EventArgs e)
        {

        }

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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(listBox1.SelectedItem.ToString());
        }
    }
}
