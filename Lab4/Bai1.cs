using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lab4
{
    public partial class Bai1 : Form
    {
        public Bai1()
        {
            InitializeComponent();
            textBox1.Text = "http://uit.edu.vn";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text.Trim();
            ShowMessage(getHTML(url)); 
        }

        private string getHTML(string szURL)
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create(szURL);
            // Get the response.   
            WebResponse response = request.GetResponse();
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.   
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.   
            string responseFromServer = reader.ReadToEnd();
            // Close the response.   
            response.Close();
            return responseFromServer;
        }

        private void ShowMessage(string mess)
        {
            richTextBox1.ReadOnly = true;
            richTextBox1.Clear();
            richTextBox1.Text = mess;
        }
    }
}
