using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            listView2.Clear();

            try
            {
                /*List<double> list = new List<double>();*/
                char[] delimiters = {',', ' '}; // Dấu phân cách

                string[] numbers = textBox1.Text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                //convert array string to double
                double[] a = new double[numbers.Length];

                for (int i = 0; i < numbers.Length; i++)
                {
                    a[i] = double.Parse(numbers[i]);
                    if (a[i] > 10 || a[i] < 0)
                    {
                        MessageBox.Show("Lỗi: Điểm không hợp lệ");
                        return; // Exit try-catch
                    }
                }

                for (int i = 0; i < numbers.Length; i++)
                {
                    listView1.Items.Add("Môn " + (i + 1) + ": " + numbers[i] + "đ");
                }

                Average_Xeploai(a);
                Min_Max(a);
                PassingGrade(a);
            }
            catch
            {
                MessageBox.Show("Lỗi: Điểm không hợp lệ!");
            }
        }

        private void Average_Xeploai(double[] a)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i];
            }
            double average = sum / a.Length;
            listView2.Items.Add("Điểm trung bình: " + Math.Round(average, 2, MidpointRounding.ToEven));

            /*int rankType = 0;                                     //Xếp loại C1
            if (average >= 8)
            {
                rankType = 1;
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] < 6.5)
                    {
                        rankType = 0;
                        break;
                    }
                }
            }
            if (average >= 6.5)
             {
                 rankType = 2;
                 for (int i = 0; i < a.Length; i++)
                 {
                     if (a[i] < 5)
                     {
                         rankType = 0;
                         break;
                     }
                 }
             }
             if (average >= 5)
             {
                 rankType = 3;
                 for (int i = 0; i < a.Length; i++)
                 {
                     if (a[i] < 3.5)
                     {
                         rankType = 0;
                         break;
                     }
                 }
             }
             if (average >= 3.5)
             {
                 rankType = 4;
                 for (int i = 0; i < a.Length; i++)
                 {
                     if (a[i] < 2)
                     {
                         rankType = 0;
                         break;
                     }
                 }
             }*/

            int rankType = 0;                                       //Xếp loại C2

            double MinResult = a[0];
            int j = 0;
            while (j < a.Length) //Tìm số nhỏ nhất
            {
                if (MinResult > a[j])
                {
                    MinResult = a[j];
                }
                j++;
            }

            if (average >= 3.5 && MinResult >= 2)
            {
                rankType = 4;
            }
            if (average >= 5 && MinResult >= 3.5)
            {
                rankType = 3;
            }
            if (average >= 6.5 && MinResult >= 5)
            {
                rankType = 2;
            }
            if (average >= 8 && MinResult >= 6.5)
            {
                rankType = 1;
            }

            string Rank;
            switch (rankType)
            {
                case 1: Rank = "Giỏi"; break;
                case 2: Rank = "Khá"; break;
                case 3: Rank = "TB"; break;
                case 4: Rank = "Yếu"; break;
                default: Rank = "Kém"; break;
            }
            listView2.Items.Add("Xếp loại học lực: " + Rank);
        }

        private void Min_Max(double[] a)
        {
            double MaxResult = a[0];
            double MinResult = a[0];
            int j = 0;
            while (j < a.Length)
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
            listView2.Items.Add("Môn có điểm cao nhất: " + MaxResult + " đ");
            listView2.Items.Add("Môn có điểm thấp nhất: " + MinResult + " đ");
        }

        private void PassingGrade(double[] a)
        {
            int count = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] >= 5)
                {
                    count++;
                }
            }

            listView2.Items.Add("Số môn không đậu: " + (a.Length - count));
            listView2.Items.Add("Số môn đậu: " + count);
        }
    }
}