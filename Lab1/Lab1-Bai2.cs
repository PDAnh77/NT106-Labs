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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control x in this.Controls)
            {
                if (x is TextBox)
                {
                    ((TextBox)x).Clear();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] a = new double[3];
            int i = 0;
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                try
                {
                    a[i] = double.Parse(((TextBox)tb).Text);
                }
                catch
                {
                    continue;
                }
                i++;
            }

            int j = 1;
            double MaxResult = a[0];
            double MinResult = a[0];
            while (j < 3)
            {
                if (MaxResult < a[j])
                {
                    MaxResult = a[j];
                }
                if (MinResult > a[j])
                {
                    MinResult = a[j];
                }
                j++;
            }
            textBox4.Text = MaxResult.ToString();
            textBox5.Text = MinResult.ToString();
        }

        private void IsNumber(TextBox tb)
        {
            int output = 0;
            if (!int.TryParse(tb.Text, out output) && tb.Text != "")
            {
                MessageBox.Show("Vui lòng nhập số nguyên!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            IsNumber(textBox1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            IsNumber(textBox2);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            IsNumber(textBox3);
        }
    }
}