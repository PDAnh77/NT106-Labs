using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num = Int32.Parse(textBox1.Text.Trim());
            string str;
            switch (num)
            {
                case 0:
                    str = "Không";
                    break;
                case 1:
                    str = "Một";
                    break;
                case 2:
                    str = "Hai";
                    break;
                case 3:
                    str = "Ba";
                    break;
                case 4:
                    str = "Bốn";
                    break;
                case 5:
                    str = "Năm";
                    break;
                case 6:
                    str = "Sáu";
                    break;
                case 7:
                    str = "Bảy";
                    break;
                case 8:
                    str = "Tám";
                    break;
                case 9:
                    str = "Chín";
                    break;
                default:
                    str = "Không đọc được số trên!";
                    break;
            }
            textBox2.Text = str;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int output = 0;
            if (!int.TryParse(textBox1.Text, out output) && textBox1.Text != "")
            {
                MessageBox.Show("Vui lòng nhập số nguyên!");
            }
        }
    }
}
