using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_Bai1
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        bool SendConnectionCheck = false;

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "127.0.0.1";
            textBox2.Text = "8080";
            int port = Int32.Parse(textBox2.Text);

            try
            {
                //Connect
                UdpClient udpClient = new UdpClient();

                string[] delimiterChars = { "\n", "\r", "\r\n" };
                string[] messages = richTextBox1.Text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                foreach (string line in messages)
                {
                    Byte[] sendBytes = Encoding.UTF8.GetBytes(line);
                    udpClient.Send(sendBytes, sendBytes.Length, textBox1.Text, port);
                }

                if (SendConnectionCheck == false)
                {
                    MessageBox.Show("Gửi tín hiệu kết nối tới server thành công!");
                    SendConnectionCheck = true;
                }
            }
            catch (Exception ex)
            {
                richTextBox1.Text += ex.Message;
            }
        }
    }
}
