using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Read_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files|*.txt";
            ofd.FilterIndex = 1;
            ofd.ShowDialog();
            try
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(fs);
                string content = sr.ReadToEnd();

                richTextBox1.ReadOnly = true;
                richTextBox1.Text = content;
                fs.Close();
                
                // Get file name//
                string name = ofd.SafeFileName.ToString();
                fileName.Text = name;

                // Get file path //
                string urf = fs.Name.ToString();
                fileUrl.Text = urf;

                richTextBox1.Text = content;

                char[] array = new char[1024];
                array = content.ToArray();

                // Count number of character in a file //
                int charCount = content.Length;
                charactesOut.Text = charCount.ToString();

                // Count number of line in a file //
                content = content.Replace("\r\n", "\r");
                int lineCount = richTextBox1.Lines.Count();
                content = content.Replace('\r', ' ');

                linesOut.Text = lineCount.ToString();

                // Count number of word in a file //
                string[] source = content.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
                int wordCount = source.Count();
                wordsOut.Text = wordCount.ToString();
            }
            catch
            {
                MessageBox.Show("Error: File not found {fileName?}");
            }
        }
    }
}