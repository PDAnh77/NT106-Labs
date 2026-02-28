using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Bai4 : Form
    {
        private WebBrowser webBrowser;

        public Bai4()
        {
            InitializeComponent();
            tbURL.Text = "http://google.com";
            webBrowser = new WebBrowser();
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Dock = DockStyle.Fill;
            Controls.Add(webBrowser);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            /*string url = toolStripComboBox1.Text.Trim();*/
            webBrowser.Navigate(tbURL.Text);
        }

        private void btnDownload_Click(object sender, EventArgs e) 
        {
            string htmlSource = webBrowser.DocumentText;
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "HTML files (*.html)|*.html|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "html";
                saveFileDialog.AddExtension = true;
                saveFileDialog.Title = "Save HTML Source";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, htmlSource);
                    MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            ViewSource webSource = new ViewSource();
            webSource.Show();
        }

        private void btnInspect_Click(object sender, EventArgs e)
        {
            string htmlSource = webBrowser.DocumentText;
            MessageBox.Show(htmlSource);
        }

        private void btnMedia_Click(object sender, EventArgs e)
        {
            string htmlSource = webBrowser.DocumentText;
            string baseUrl = webBrowser.Url.ToString();
            var imageUrls = GetImageUrls(htmlSource, baseUrl);

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderBrowserDialog.SelectedPath;

                    int index = 0;
                    foreach (string imageUrl in imageUrls)
                    {
                        SaveImage(imageUrl, folderPath, index++);
                    }

                    MessageBox.Show("Images saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private List<string> GetImageUrls(string htmlSource, string baseUrl)
        {
            var imageUrls = new List<string>();
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlSource);

            var imgNodes = doc.DocumentNode.SelectNodes("//img");

            if (imgNodes != null)
            {
                imageUrls.AddRange(imgNodes.Select(node =>
                {
                    var src = node.GetAttributeValue("src", string.Empty);
                    if (!string.IsNullOrEmpty(src) && !Uri.IsWellFormedUriString(src, UriKind.Absolute))
                    {
                        src = new Uri(new Uri(baseUrl), src).ToString();
                    }
                    return src;
                }).Where(src => !string.IsNullOrEmpty(src)));
            }

            return imageUrls;
        }
        private void SaveImage(string imageUrl, string folderPath, int index)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    string fileName = index.ToString() + ".jpg";
                    string filePath = Path.Combine(folderPath, fileName);
                    webClient.DownloadFile(imageUrl, filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving image {imageUrl}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
