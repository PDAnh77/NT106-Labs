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

namespace Lab3_Bai3
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        TcpClient tcpClient = new TcpClient();

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (tcpClient.Connected)
            {
                try
                {
                    string text = richTextBox1.Text;
                    NetworkStream networkStream = tcpClient.GetStream();

                    if (text.Trim() == "quit")
                    {
                        networkStream.Close();
                        CloseConnection();
                    }
                    else
                    {
                        Byte[] data = Encoding.UTF8.GetBytes(text);
                        networkStream.Write(data, 0, data.Length);
                    }
                    richTextBox1.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Connect to server first!", "Error");
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            CloseConnection();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (tcpClient.Connected)
            {
                MessageBox.Show("Already connected to server", "Error");
            }
            else
            {
                Connect_to_Server();
            }
        }

        private void CloseConnection()
        {
            tcpClient.Close();
        }

        private void Connect_to_Server()
        {
            try
            {
                tcpClient = new TcpClient();
                IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
                IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 8080);
                tcpClient.Connect(iPEndPoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

    }
}
