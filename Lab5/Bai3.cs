using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailKit.Security;

namespace Lab5
{
    public partial class Bai3 : Form
    {
        public Bai3()
        {
            InitializeComponent();
            textBox3.UseSystemPasswordChar = true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string emailAddressFrom = textBox1.Text.Trim();
            string emailAddressTo = textBox2.Text.Trim();
            string password = textBox3.Text.Trim();
            string subject = textBox4.Text.Trim();
            string body = richTextBox1.Text.Trim();

            await SendEmailAsync(emailAddressFrom, emailAddressTo, password, subject, body);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox5.Text = openFileDialog.FileName;
                    MessageBox.Show("File added successfully!");
                }
            }
        }

        private async Task SendEmailAsync(string emailFrom, string emailTo, string pass, string subject, string content)
        {
            var emailSend = new MimeMessage();
            emailSend.From.Add(MailboxAddress.Parse(emailFrom));
            emailSend.To.Add(MailboxAddress.Parse(emailTo));
            emailSend.Subject = subject;

            // Create the text part
            var textPart = new TextPart(TextFormat.Plain)
            {
                Text = content
            };

            string filePath = textBox5.Text.Trim();

            // Create a multipart/mixed container to hold the text and attachment
            var multipart = new Multipart("mixed");
            multipart.Add(textPart);

            // Add the attachment if there is one
            if (!string.IsNullOrEmpty(filePath))
            {
                var attachmentPart = new MimePart()
                {
                    Content = new MimeContent(File.OpenRead(filePath)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(filePath)
                };
                multipart.Add(attachmentPart);
            }

            // Set the email body to the multipart container
            emailSend.Body = multipart;

            using (var emailClient = new SmtpClient())
            {
                try
                {
                    await emailClient.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    await emailClient.AuthenticateAsync(emailFrom, pass);
                    await emailClient.SendAsync(emailSend);
                    MessageBox.Show("Email sent successfully!");
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

    }
}
