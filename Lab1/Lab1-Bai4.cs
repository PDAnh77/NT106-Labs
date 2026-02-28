using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem?.ToString().Trim())
            {
                case "Binary":
                    Binary_Convert(textBox1);
                    break;
                case "Decimal":
                    Decimal_Convert(textBox1);
                    break;
                case "Hexadecimal":
                    Hexadecimal_Convert(textBox1);
                    break;
                default:
                    textBox2.Text = textBox1.Text;
                    break;
            }
        }

        private void Binary_Convert(TextBox n)
        {
            try
            {
                string binaryInput = textBox1.Text.Trim();
                long decimalResult = Convert.ToInt64(binaryInput, 2);

                switch (comboBox2.SelectedItem?.ToString().Trim())
                {
                    case "Decimal":
                        textBox2.Text = decimalResult.ToString();
                        break;
                    case "Hexadecimal":
                        textBox2.Text = decimalResult.ToString("X"); // ToString("X"): output format theo thập lục phân chữ hoa
                        break;
                    default:
                        textBox2.Text = textBox1.Text;
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Error: Invalid binary string");
            }
        }
         
        private void Decimal_Convert(TextBox n)
        {
            try
            {
                long decimalValue = Int64.Parse(textBox1.Text.Trim());
                string binary = Convert.ToString(decimalValue, 2);

                switch (comboBox2.SelectedItem?.ToString().Trim())
                {
                    case "Binary":
                        textBox2.Text = binary;
                        break;
                    case "Hexadecimal":
                        textBox2.Text = decimalValue.ToString("X");
                        break;
                    default:
                        textBox2.Text = textBox1.Text;
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Error: Invalid decimal string");
            }
        }

        private void Hexadecimal_Convert(TextBox n)
        {
            try
            {
                string hexInput = textBox1.Text.Trim();
                long decimalResult = Convert.ToInt64(hexInput, 16); // Cơ số input string là 16 (hệ thập lục phân)
                string binaryResult = Convert.ToString(decimalResult, 2); // Cơ số output string là 2 (hệ nhị phân)

                switch (comboBox2.SelectedItem?.ToString().Trim())
                {
                    case "Binary":
                        textBox2.Text = binaryResult;
                        break;
                    case "Decimal":
                        textBox2.Text = decimalResult.ToString();
                        break;
                    default:
                        textBox2.Text = textBox1.Text;
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Error: Invalid hexadecimal string");
            }
        }
    }
}