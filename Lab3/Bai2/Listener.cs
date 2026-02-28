using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Lab3_Bai2
{
    public partial class Listener : Form
    {
        public Listener()
        {
            InitializeComponent();
        }

        private delegate void SafeCallDelegate(string text);

        private void button1_Click(object sender, EventArgs e)
        {
            Thread serverThread = new Thread(new ThreadStart(StartThread));
            serverThread.Start();
            Thread.Sleep(1000);
        }

        async void StartThread()
        {
            try
            {
                int bytesReceived = 0;

                byte[] recv = new byte[256];

                Socket clientSocket;

                Socket listenerSocket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp
                    );

                string ipadd = "127.0.0.1";
                IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse(ipadd), 8080);

                try
                {
                    // Lấy thông tin về máy tính hiện tại
                    IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                    // Lặp qua danh sách địa chỉ IP
                    foreach (IPAddress ip in host.AddressList)
                    {
                        // Kiểm tra xem địa chỉ IP có phải là IPv4 và sử dụng nó trong IPEndPoint
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipepServer = new IPEndPoint(ip, 8080);
                            MessageBox.Show("Địa chỉ IP của máy tính: " + ip.ToString());
                            break;
                        }
                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }


                listenerSocket.Bind(ipepServer);

                listenerSocket.Listen(0);

                clientSocket = listenerSocket.Accept();

                bool connectionCheck = true;
                WriteTextSafe("New client connected");
                WriteTextSafe($"Telnet running on {ipepServer.Address}:{ipepServer.Port}");

                while (clientSocket.Connected && connectionCheck == true)
                {
                    string text = "";
                    do
                    {
                        bytesReceived = await clientSocket.ReceiveAsync(recv, SocketFlags.None);                
                        text += Encoding.UTF8.GetString(recv);
                        if(bytesReceived == 0)
                        {
                            connectionCheck = false;
                            break;
                        }
                    } while (bytesReceived == 0);
                    WriteTextSafe(text);
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
