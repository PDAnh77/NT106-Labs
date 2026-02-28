using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Drawing.Text;

namespace Lab5
{
    public partial class Bai2 : Form
    {
        List<MimeMessage> emails = new List<MimeMessage>();

        public Bai2()
        {
            InitializeComponent();
            textBox1.Text = "ducanh@nhomAC.nt106";
            textBox2.Text = "123456pdA@";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();
            string password = textBox2.Text;

            emails.Clear();
            listView1.Clear();
            label3.Text = $"Total:";
            label4.Text = $"Recent:";

            using (var client = new ImapClient())
            {
                try
                {
                    await client.ConnectAsync("127.0.0.1", 143, SecureSocketOptions.None); // Connect to the mail server using IP and port
                    await client.AuthenticateAsync(email, password);
                    var inbox = client.Inbox; // Get the inbox folder
                    await inbox.OpenAsync(FolderAccess.ReadOnly);

                    label3.Text = $"Total:      {inbox.Count}";
                    label4.Text = $"Recent:      {inbox.Recent}";

                    listView1.Columns.Add("Email", 220);
                    listView1.Columns.Add("From", 190);
                    listView1.Columns.Add("Thời gian", 120);
                    listView1.View = View.Details;
                    for (int i = 0; i < inbox.Count; i++)
                    {
                        var message = await inbox.GetMessageAsync(i);
                        emails.Add(message);  // Store the message

                        ListViewItem name = new ListViewItem(message.Subject);

                        ListViewItem.ListViewSubItem from = new ListViewItem.ListViewSubItem(name, message.From.ToString());
                        name.SubItems.Add(from);

                        ListViewItem.ListViewSubItem date = new ListViewItem.ListViewSubItem(name, message.Date.Date.ToString());
                        name.SubItems.Add(date);
                        listView1.Items.Add(name);
                    }
                }
                catch (ImapCommandException ex)
                {
                    MessageBox.Show($"IMAP Command Error: {ex.Message}");
                }
                catch (ImapProtocolException ex)
                {
                    MessageBox.Show($"IMAP Protocol Error: {ex.Message}");
                }
                catch (SocketException ex)
                {
                    MessageBox.Show($"Network Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"General Error: {ex.Message}");
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                int selectedIndex = listView1.SelectedIndices[0];
                var selectedEmail = emails[selectedIndex];

                // Extract the body of the email
                string emailBody = GetEmailBody(selectedEmail);

                ShowContent(emailBody); // Display the email content
            }
        }

        private string GetEmailBody(MimeMessage message)
        {
            if (!string.IsNullOrEmpty(message.TextBody))
            {
                return message.TextBody;
            }
            else if (!string.IsNullOrEmpty(message.HtmlBody))
            {
                return message.HtmlBody;
            }
            else if (message.Body is TextPart textPart)
            {
                return textPart.Text;
            }
            else if (message.Body is Multipart multipart)
            {
                foreach (var part in multipart)
                {
                    if (part is TextPart multipartTextPart)
                        return multipartTextPart.Text;
                }
            }
            return "No content available";
        }

        private void ShowContent(string emailContent)
        {
            Form form = new Form
            {
                Size = this.Size,
            };
            RichTextBox EmailContent = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Text = emailContent,
                ScrollBars = RichTextBoxScrollBars.Vertical
            };
            form.Controls.Add(EmailContent);
            form.ShowDialog();
        }
    }
}