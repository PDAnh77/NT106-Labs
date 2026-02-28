using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_Bai3
{
    public partial class Listener : Form
    {
        public Listener()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread serverThread = new Thread(new ThreadStart(StartThread));
            serverThread.Start();
            Thread.Sleep(1000);
        }

        private delegate void SafeCallDelegate(string text);

        private void StartThread()
        {
            try
            {
                int bytesReceived = 0;

                byte[] recv = new byte[1024];

                Socket clientSocket;

                Socket listenerSocket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp
                    );
                string ipadd = "127.0.0.1";
                IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse(ipadd), 8080);

                listenerSocket.Bind(ipepServer);
                WriteTextSafe($"Server running on {ipepServer.Address}:{ipepServer.Port}");

                listenerSocket.Listen(0);

                WriteTextSafe("Waiting for a connection...");
                clientSocket = listenerSocket.Accept();


                WriteTextSafe("New client connected!");


                while (clientSocket.Connected)
                {
                    string text = "";

                    bytesReceived = clientSocket.Receive(recv);
                    
                    text = Encoding.UTF8.GetString(recv, 0, bytesReceived);

                    if (bytesReceived == 0)
                    {
                        break;
                    }

                    string[] delimiterChars = { "\n", "\r", "\r\n" };
                    string[] messages = text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string message in messages)
                    {
                        WriteTextSafe($"{ipepServer.Address}:{ipepServer.Port}: {message}");
                    }
                    
                    Array.Clear(recv, 0, recv.Length);
                }
                MessageBox.Show("Client has disconnected");
                listenerSocket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

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
