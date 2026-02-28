using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Bai2 : Form
    {
        public Bai2()
        {
            InitializeComponent();
            textBox1.Text = "https://httpbin.org/post";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text.Trim();
            ShowContent(postHTML(url));
        }

        private string postHTML (string szURL)
        {
            string responseFromServer = "";
            try
            {
                string mess = textBox2.Text.Trim();
                WebRequest request = WebRequest.Create(szURL);
    
                request.Method = "POST";
                byte[] byteArray = Encoding.UTF8.GetBytes(mess);
                request.ContentLength = byteArray.Length;
                request.ContentType = "text/plain";
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(byteArray, 0, byteArray.Length);
                }

                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
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
