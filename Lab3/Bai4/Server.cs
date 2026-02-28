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
using static System.Net.Mime.MediaTypeNames;

namespace Lab3_Bai4
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        Socket listenerSocket;
        bool isServerRunning = false;
        List<Socket> connectedClients;

        private void btnListen_Click(object sender, EventArgs e)
        {
            try
            {
                connectedClients = new List<Socket>();

                listenerSocket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp);

                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                int port = 8080;
                IPEndPoint ipepServer = new IPEndPoint(ipAddress, port);

                listenerSocket.Bind(ipepServer);
                WriteTextSafe($"Server running on {ipepServer.Address}:{ipepServer.Port}");
                listenerSocket.Listen(0);
                isServerRunning = true;
                WriteTextSafe("Waiting for a connection...");

                Thread listenThread = new Thread(new ThreadStart(ListenForClients));
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ListenForClients()
        {
            try
            {
                while (isServerRunning)
                {
                    Socket clientSocket = listenerSocket.Accept(); // Error: A blocking operation was interrrupted by a call to WSACancelBlockingCall
                    connectedClients.Add(clientSocket);
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.Start(clientSocket);
                }
            }
            catch (SocketException sx)
            {
                /*MessageBox.Show(sx.Message, "Error");*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void HandleClientComm(object? clientSocket)
        {
            if (clientSocket != null)
            {
                Socket socket = (Socket)clientSocket;
                byte[] recv = new byte[1024];
            
                if (socket.RemoteEndPoint != null)
                {
                    string clientIP = ((IPEndPoint)socket.RemoteEndPoint).Address.ToString();
                    int clientPort = ((IPEndPoint)socket.RemoteEndPoint).Port;
                    WriteTextSafe($"New client connected from: {clientIP}:{clientPort}");

                    try
                    {
                        while (isServerRunning)
                        {
                            int bytesReceived = socket.Receive(recv);
                            if (bytesReceived == 0)
                            {
                                break;
                            }

                            string text = Encoding.UTF8.GetString(recv, 0, bytesReceived);

                            string[] delimiterChars = { "\n", "\r", "\r\n" };
                            string[] messages = text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string message in messages)
                            {
                                WriteTextSafe($"{clientIP}:{clientPort}: {message}");
                                Send_to_Client(message);
                            }

                            Array.Clear(recv, 0, recv.Length);
                        }
                        WriteTextSafe($"{clientIP}:{clientPort} has disconnected");
                        Send_to_Client($"{clientIP}:{clientPort} has disconnected");
                        connectedClients.Remove(socket);
                        socket.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
        }

        private void Send_to_Client(string mess)
        {
            if(isServerRunning)
            {
                for (int i = 0; i < connectedClients.Count; i++)
                {
                    connectedClients[i].Send(Encoding.UTF8.GetBytes(mess));
                }
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

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if(isServerRunning == false)
            {
                WriteTextSafe("Server hasn't started!");
            }
            else
            {
                Send_to_Client("Disconnected from server");
                isServerRunning = false;
                WriteTextSafe("Server stopped");
                listenerSocket.Close();
            }
        }
    }
}
