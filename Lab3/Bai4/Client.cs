using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_Bai4
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        TcpClient tcpClient = new TcpClient();
        NetworkStream networkStream;
        Thread IncomingMess;

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (tcpClient.Connected)
            {
                MessageBox.Show("Already connected to server", "Error");
            }
            else
            {
                Connect_to_Server();
                IncomingMess = new Thread(new ThreadStart(ReceivedMessages));
                IncomingMess.IsBackground = true;
                IncomingMess.Start();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (tcpClient.Connected)
            {
                try
                {
                    string hostname = textBox1.Text.Trim();

                    if (string.IsNullOrEmpty(hostname))
                    {
                        hostname = "anonymous";
                    }
                    string text = ($"{hostname}: {textBox2.Text}");

                    if (textBox2.Text.Trim() == "quit")
                    {
                        CloseConnection();
                        WriteTextSafe("Disconnected successfully");
                    }
                    else
                    {
                        Byte[] data = Encoding.UTF8.GetBytes(text);
                        networkStream.Write(data, 0, data.Length);
                    }
                    textBox2.Clear();
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

        private void ReceivedMessages()
        {
            try
            {
                while (tcpClient.Connected)
                {
                    byte[] recv = new byte[1024];

                    networkStream = tcpClient.GetStream();
                    int bytesReceived = 0;
                    try
                    {
                        bytesReceived = networkStream.Read(recv, 0, recv.Length);
                    }
                    catch
                    {
                        continue;
                    }

                    if (bytesReceived == 0)
                    {
                        continue;
                    }

                    string text = Encoding.UTF8.GetString(recv, 0, bytesReceived);
                    string[] delimiterChars = { "\n", "\r", "\r\n" };
                    string[] messages = text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                    // Display messages received from the server
                    foreach (string message in messages)
                    {
                        WriteTextSafe(message);
                    }

                    if (text == "Disconnected from server")
                    {
                        CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CloseConnection()
        {
            IncomingMess.Interrupt();
            networkStream.Close();
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
                listView1.Items.Add("Client is connected!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private delegate void SafeCallDelegate(string text);

        private void WriteTextSafe(string text)
        {
            if (this.listView1.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                listView1.Items.Add(new ListViewItem(text));
            }
        }
    }
}
