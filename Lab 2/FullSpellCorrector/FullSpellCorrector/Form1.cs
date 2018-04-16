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

/// <summary>
/// 
/// </summary>
namespace FullSpellCorrector
{
    
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1" /> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            checkBox1.Checked = true;

            while (!sr.EndOfStream)
            {
                words.Add(sr.ReadLine());
            }

        }


        /// <summary>
        /// The sr
        /// </summary>
        StreamReader sr = new StreamReader("words");
        /// <summary>
        /// The words
        /// </summary>
        List<string> words = new List<string>();
        /// <summary>
        /// The answers
        /// </summary>
        List<string> answers = new List<string>();

        /// <summary>
        /// The selected
        /// </summary>
        string selected = "";

        /// <summary>
        /// The mini
        /// </summary>
        int mini = 0;

        /// <summary>
        /// Handles the TextChanged event of the richTextBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string text = "";
            string word = "";
            int nn = 0, n;

            if (text == richTextBox1.Text) return;
            text = richTextBox1.Text;

            n = richTextBox1.SelectionStart;

            richTextBox1.SelectionColor = Color.Black;

            if (n - 1 > 0 && text != "" && !Char.IsLetter(text[n - 1]) && !char.IsDigit(text[n - 1]))
            {
                for (int i = n - 2; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        word = text.Substring(i, n - i - 1);
                        break;
                    }
                    if (!Char.IsLetter(text[i]) && !char.IsDigit(text[i]))
                    {
                        word = text.Substring(i + 1, n - i - 2);
                        break;
                    }

                }
                if (word == "") return;

                if (words.Contains(word) || char.IsNumber(word[0]))
                {
                    return;
                }
                else
                {
                    if (checkBox1.Checked)
                    {
                        boxChecked(text, word, n);
                    }
                    else
                    {
                        boxNotChecked(word, n);

                    }
                }
            }
        }

        /// <summary>
        /// Boxes the checked.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="word">The word.</param>
        /// <param name="n">The n.</param>
        public void boxChecked(string text, string word, int n)
        {
            string ans = LeivenshteinAlgorithm(word);

            text = ReplaceWord(text, word, ans, n - word.Length - 1);

            richTextBox1.Text = text; 
            richTextBox1.SelectionStart = n - word.Length + ans.Length;
        }

        /// <summary>
        /// Boxes the not checked.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="n">The n.</param>
        public void boxNotChecked(string word, int n)
        {


            richTextBox1.SelectionStart = n - word.Length - 1;
            richTextBox1.SelectionLength = word.Length;
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.SelectionStart = n;
            richTextBox1.SelectionLength = 0;

        }




        /// <summary>
        /// Leivenshteins the algorithm.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public string LeivenshteinAlgorithm(string word)
        {
            string s_comp, ans = "";
            mini = 1000;
            int[,] dp = null;

            for (int k = 0; k < words.Count; k++)
            {
                s_comp = words[k];
                dp = new int[word.Length + 1, s_comp.Length + 1];
                for (int i = 0; i <= word.Length; i++)
                {
                    dp[i, 0] = i;
                }
                for (int j = 0; j <= s_comp.Length; j++)
                {
                    dp[0, j] = j;
                }

                for (int i = 1; i <= word.Length; i++)
                {
                    for (int j = 1; j <= s_comp.Length; j++)
                    {
                        if (word[i - 1] == s_comp[j - 1])
                        {
                            dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1]);
                        }
                        else
                        {
                            dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + 2);
                        }
                    }
                }

                if (dp[word.Length, s_comp.Length] <= mini)
                {
                    mini = dp[word.Length, s_comp.Length];
                    ans = s_comp;

                    if (mini <= 5)
                    {
                        answers.Add(ans);
                    }
                }
            }


            return ans;
        }

        /// <summary>
        /// Replaces the word.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="word">The word.</param>
        /// <param name="ans">The ans.</param>
        /// <param name="n">The n.</param>
        /// <returns></returns>
        public string ReplaceWord(string text, string word, string ans, int n)
        {
            int i = n;
            string s = text.Remove(n, word.Length);

            string k = s.Insert(n, ans);
            return k;
        }


        /// <summary>
        /// Handles the CheckedChanged event of the checkBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button1.Visible = false;
                button2.Visible = false;

            }
            else
            {
                button2.Visible = true;
                button1.Visible = true;
            }

        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the richTextBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void richTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            richTextBox1.AutoWordSelection = true;
            string word = richTextBox1.SelectedText;
            selected = word;

            contextMenuStrip1.Items.Clear();
            int n = richTextBox1.SelectionStart + word.Length;

            string text = richTextBox1.Text;
            answers.Clear();
            string lg = LeivenshteinAlgorithm(word);
            if (answers.Count > 0)
            {
                for (int i = 0; i < answers.Count; i++)
                {
                    contextMenuStrip1.Items.Add(answers[i]);
                }
            }
            if (contextMenuStrip1.Items.Count == 0) contextMenuStrip1.Items.Add(lg);

            contextMenuStrip1.Show(Cursor.Position);


        }


        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionColor = Color.Black;
        }



        /// <summary>
        /// Handles the ItemClicked event of the contextMenuStrip1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ToolStripItemClickedEventArgs"/> instance containing the event data.</param>
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string clicked = e.ClickedItem.Text;

            contextMenuStrip1.Visible = true;
            string text = richTextBox1.Text;
            int n = richTextBox1.SelectionStart;


            string word = richTextBox1.SelectedText.Remove(richTextBox1.SelectionLength - 1, 1);

            text = ReplaceWord(text, word, clicked, n);
            richTextBox1.Text = text;
            richTextBox1.Select(n, clicked.Length);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.SelectionStart = n + clicked.Length;
            richTextBox1.SelectionLength = 0;


        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            words.Add(selected);

            richTextBox1.SelectionColor = Color.Black;
            selected = "";

        }

        /// <summary>
        /// Handles the Click event of the button4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}