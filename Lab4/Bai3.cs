using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Bai3 : Form
    {
        public Bai3()
        {
            InitializeComponent();
            textBox2.Text = "C:\\Users\\anhph\\Downloads\\testBai3_LTM\\";
            textBox1.Text = "http://uit.edu.vn";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text.Trim();
            ShowContent(downloadHTML(url));
        }

        private string downloadHTML(string url)
        {
            string responseFromServer = "";
            try
            {
                string downloadPath = textBox2.Text.Trim();

                WebClient myClient = new WebClient();
                myClient.DownloadFile(url, downloadPath);

                Stream respone = myClient.OpenRead(url);
                StreamReader reader = new StreamReader(respone, Encoding.UTF8);
                responseFromServer = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return responseFromServer;
        }

        private void ShowContent(string data)
        {
            richTextBox1.ReadOnly = true;
            richTextBox1.Clear();
            richTextBox1.Text = data;
        }
    }
}
