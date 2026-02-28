using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // File read btn
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            try
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(fs);
                string content = sr.ReadToEnd();
                richTextBox1.Text = content;
                fs.Close();
            }
            catch
            {
                MessageBox.Show("Không tìm được file!");
            }
        }

        private void button2_Click(object sender, EventArgs e) // File write btn
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            try
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                string text = richTextBox1.Text;
                sw.WriteLine(text.ToUpper());
                sw.Flush();
                fs.Close();
            }
            catch
            {
                MessageBox.Show("Không tìm được file!");
            }
        }

        private void button3_Click(object sender, EventArgs e) // Exit btn
        {
            Close();
        }
    }
}