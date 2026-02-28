using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab3_Bai1
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        delegate void SafeCallDelegate(string text);

        public void serverThread()
        {
            int port = Int32.Parse(textBox1.Text);
            UdpClient udpClient = new UdpClient(port);
            while (true)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, port);
                Byte[] receivedBytes = udpClient.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.UTF8.GetString(receivedBytes);
                if (returnData != null)
                {
                    string mess = RemoteIpEndPoint.ToString() + ": " + returnData.ToString();
                    WriteTextSafe(mess);
                }                    
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = "8080";
                Thread thdUDPServer = new Thread(new ThreadStart(serverThread));
                listView1.Items.Add("Kết nối với client thành công!");
                thdUDPServer.Start();
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                listView1.Items.Add($"{ex.Message}");
            }
        }

        private void WriteTextSafe(string text)
        {
            if (listView1.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                listView1.Invoke(d, new object[] { text });
            }
            else
            {
                listView1.Items.Add (text);
            }
        }
    }
}
