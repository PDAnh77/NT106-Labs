using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Org.BouncyCastle.Crypto.Macs;

namespace Lab6
{
    public partial class Server : Form
    {
        bool isServerRunning = false;
        List<Socket> connectedClients;
        Socket listenerSocket;
        delegate void SafeCallDelegate(string text, Control control);

        public Server()
        {
            InitializeComponent();

            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            connectedClients = new List<Socket>();
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                int port = 8080;
                IPEndPoint ipepServer = new IPEndPoint(ipAddress, port);

                listenerSocket.Bind(ipepServer);
                WriteTextSafe($"Server running on {ipepServer.Address}:{ipepServer.Port}", listView1);
                listenerSocket.Listen(0);
                isServerRunning = true;
                WriteTextSafe("Waiting for a connection...", listView1);

                Thread listenThread = new Thread(new ThreadStart(ListenForClients));
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private async void ListenForClients()
        {
            try
            {
                bool acceptClient = true; // Accept new client
                while (isServerRunning)
                {
                    if (!acceptClient)
                    {
                        continue;
                    }

                    Socket clientSocket = listenerSocket.Accept();
                    connectedClients.Add(clientSocket);
                    BroadcastClientCount(); // Sends the number of connected clients
                    Thread clientThread = new Thread(() => HandleClient(clientSocket)); // Create a thread for each client connected
                    clientThread.Start();

                    if (connectedClients.Count == 5)
                    {
                        await SendEmail();
                        acceptClient = false;
                        WriteTextSafe("Temporarily stop accepting new client connections.", listView1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                StopServer();
            }
        }

        private void HandleClient(Socket clientSocket)
        {
            WriteTextSafe($"Number of clients: {connectedClients.Count}", label1);

            if (clientSocket == null || clientSocket.RemoteEndPoint == null)
            {
                return;
            }

            byte[] recv = new byte[1024];

            string clientIP = ((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString();
            int clientPort = ((IPEndPoint)clientSocket.RemoteEndPoint).Port;
            WriteTextSafe($"New client connected from: {clientIP}:{clientPort}", listView1);

            try
            {
                while (isServerRunning)
                {
                    int bytesReceived = clientSocket.Receive(recv);
                    if (bytesReceived == 0)
                    {
                        break;
                    }

                    string text = Encoding.UTF8.GetString(recv, 0, bytesReceived);
                    // Broadcast the received message to all connected clients
                    BroadcastToClients(text, clientSocket);

                    Array.Clear(recv, 0, recv.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                WriteTextSafe($"{clientIP}:{clientPort} has disconnected", listView1);
                connectedClients.Remove(clientSocket);

                WriteTextSafe($"Number of clients: {connectedClients.Count}", label1);
                BroadcastClientCount();
                clientSocket.Close();
            }
        }

        private void BroadcastToClients(string data, Socket sender)
        {
            lock (connectedClients)
            {
                foreach (var clientSocket in connectedClients)
                {
                    if (clientSocket != sender && clientSocket.Connected)
                    {
                        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                        clientSocket.Send(dataBytes);
                    }
                }
            }
        }

        private void BroadcastClientCount()
        {
            string clientCountMessage = $"Number of clients|{connectedClients.Count}";
            lock (connectedClients)
            {
                foreach (var clientSocket in connectedClients)
                {
                    if (clientSocket.Connected)
                    {
                        byte[] dataBytes = Encoding.UTF8.GetBytes(clientCountMessage);
                        clientSocket.Send(dataBytes);
                    }
                }
            }
        }

        private void StopServer()
        {
            if (listenerSocket != null)
            {
                isServerRunning = false;
                listenerSocket.Close();

                lock (connectedClients)
                {
                    foreach (var clientSocket in connectedClients)
                    {
                        clientSocket.Close();
                    }
                    connectedClients.Clear();
                }

                WriteTextSafe("Server stopped.", listView1);
            }
        }

        private async Task SendEmail()
        {
            var emailSend = new MimeMessage();
            emailSend.From.Add(MailboxAddress.Parse("anhphamduc52@gmail.com"));
            emailSend.To.Add(MailboxAddress.Parse("22520067@gm.uit.edu.vn"));
            emailSend.Subject = "Number of clients warning!";
            TextPart content = new TextPart(TextFormat.Html)
            {
                Text = "The current number of connected clients has reached 5."
            };
            emailSend.Body = content;

            using (var emailClient = new SmtpClient())
            {
                try
                {
                    await emailClient.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    await emailClient.AuthenticateAsync("anhphamduc52@gmail.com", "oqad wkhy iwik eetn");
                    await emailClient.SendAsync(emailSend);
                    MessageBox.Show("Email sent to administrator successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    await emailClient.DisconnectAsync(true);
                }
            }
        }

        private void WriteTextSafe(string text, Control control)
        {
            if (control.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                control.Invoke(d, new object[] { text, control });
            }
            else
            {
                if (control is ListView listview)
                {
                    listview.Items.Add(text);
                }
                if (control is Label label)
                {
                    label.Text = text;
                }
            }
        }
    }
}
