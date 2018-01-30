using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FullSpellCorrector
{
    [Serializable]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            checkBox1.Checked = true;
            button4.Visible = true;
        }

       
        static List<String> words = null;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;    
            }
                else
            {
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string text = " ";
            string word = " ";
            if (text == richTextBox1.Text) return;
            text = richTextBox1.Text;
            int n = text.Length;
            if(!Char.IsLetter(text[n-1]))
            {
                for(int i = n - 2; i >= 0; i--)
                {
                    if(i == 0) { word = text.Substring(i, n - i - 1); break; }
                    if(!Char.IsLetter(text[i])) { word = text.Substring(i + 1, n - i - 2); break; }
                }
            }
        }






        static void Serialize()
        {
            FileStream fs = new FileStream("words.dat", FileMode.Create);

            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                bf.Serialize(fs, words);
            }
            catch(SerializationException se)
            {
                System.Diagnostics.Debug.Write(se.Message);
                throw;
            }

            finally
            {
                fs.Close();
            }
        }
        static void Deserialize()
        {
            FileStream fs = new FileStream("words.dat", FileMode.Open);
            try
            {
                BinaryFormatter bf = new BinaryFormatter();

                words = (List<string>)bf.Deserialize(fs);
            }
            catch (SerializationException se)
            {
                System.Diagnostics.Debug.Write(se.Message);
                throw;
            }

            finally
            {
                fs.Close();
            }
        }
    }
}
