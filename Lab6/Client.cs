using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Client : Form
    {
        // Whiteboard
        List<Point> points = new List<Point>(); // List to store points
        Bitmap drawingBitmap;
        Graphics graphics;
        Stack<Bitmap> undoStack = new Stack<Bitmap>(); // Store the states of the drawing before any changes are made
        Stack<Bitmap> redoStack = new Stack<Bitmap>(); // Store the current states after any changes are made

        // Drawing pen
        bool isDrawing = false;
        int Linewidth = 1;
        Color penColor = Color.Black;

        // Image
        private Dictionary<Image, Rectangle> images = new Dictionary<Image, Rectangle>();
        Image selectedImage = null;
        Rectangle selectedImageRect;
        Rectangle resizeHandle;
        bool isAssigningImage = false;
        bool isDragging = false;
        bool isResizing = false;
        Point dragStartPoint; // Position when dragging started
        Point resizeStartPoint;
        const int handleSize = 10; // Size of the resize handle

        // TCP connection
        TcpClient tcpClient = new TcpClient();
        NetworkStream stream;
        byte[] buffer = new byte[1024];
        int clientCount = 1;
        static readonly object saveimglock = new object();
        delegate void SafeCallDelegate(string text, Control control);

        public Client()
        {
            InitializeComponent();
            InitializeConnection();

            // Initialize the drawing area.
            drawingBitmap = new Bitmap(Whiteboard.Width, Whiteboard.Height);
            graphics = Graphics.FromImage(drawingBitmap);
            graphics.Clear(Color.White);
            Whiteboard.Image = drawingBitmap;

            this.KeyPreview = true; // Allows the form to handle key events even if a control (like a TextBox or Button) has focus
            radioButton1.Checked = true;

            // Start listening for server data
            Task.Run(() => ReceivedFromServer());
        }

        private void InitializeConnection()
        {
            // Connect to server
            try
            {
                IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
                IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 8080);
                tcpClient.Connect(iPEndPoint);
                stream = tcpClient.GetStream();
                /*MessageBox.Show("Client is connected!");*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                Application.Exit();
            }
        }

        private void Whiteboard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                selectedImage = null;

                foreach (Image image in images.Keys)
                {
                    if (images[image].Contains(e.Location))
                    {
                        selectedImage = image;
                        selectedImageRect = images[image];
                        break;
                    }
                }

                if (selectedImage != null)
                {
                    if (resizeHandle.Contains(e.Location)) // Check if click on the resize handle
                    {
                        isResizing = true;
                        resizeStartPoint = e.Location;
                    }
                    else
                    {
                        isDragging = true;
                        dragStartPoint = e.Location;
                    }
                }
                else
                {
                    isDrawing = true;
                    points.Clear(); // Clear previous points
                    points.Add(e.Location); // Store the beginning point when the user presses the left mouse button down
                    SaveStateForUndo(); // Save current state for undo

                    // Send start drawing signal to the server
                    SendPointData(e.Location, "start");
                }
                Whiteboard.Invalidate();
            }
        }

        private void Whiteboard_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = string.Format("{0}, {1}", e.Location.X, e.Location.Y);

            if (selectedImage != null)
            {
                if (isResizing)
                {
                    // Calculate the new size based on the mouse movement
                    int newWidth = selectedImageRect.Width + (e.X - resizeStartPoint.X);
                    int newHeight = selectedImageRect.Height + (e.Y - resizeStartPoint.Y);

                    // Prevent the image from being resized too small
                    if (newWidth > handleSize && newHeight > handleSize)
                    {
                        selectedImageRect = new Rectangle(selectedImageRect.Location, new Size(newWidth, newHeight));
                        images[selectedImage] = selectedImageRect;
                        Whiteboard.Invalidate();
                    }
                    resizeStartPoint = e.Location;
                }
                else if (isDragging)
                {
                    // Calculate the distance (offset) the image has moved since the dragging started (dragStartPoint)
                    Point offset = new Point(e.Location.X - dragStartPoint.X, e.Location.Y - dragStartPoint.Y);
                    selectedImageRect.Location = new Point(selectedImageRect.X + offset.X, selectedImageRect.Y + offset.Y);
                    images[selectedImage] = selectedImageRect;
                    dragStartPoint = e.Location;
                    Whiteboard.Invalidate();
                }
            }

            if (isDrawing)
            {
                points.Add(e.Location); // Stores the points where the mouse has moved while drawing
                Whiteboard.Invalidate(); // Triggers a repaint to show the drawing preview

                // Sends each point data to the server
                SendPointData(e.Location);
            }
        }

        private void Whiteboard_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (selectedImage != null)
                {
                    if (isResizing)
                    {
                        isResizing = false;
                    }
                    if (isDragging)
                    {
                        isDragging = false;
                    }
                }

                if (isDrawing)
                {
                    isDrawing = false;
                    points.Add(e.Location);
                    DrawLine(points, Linewidth, penColor);
                    SaveStateForRedo();
                    points.Clear(); // Clear points after drawing

                    // Send end drawing signal to the server
                    SendPointData(e.Location, "end");
                }
                Whiteboard.Invalidate();
            }
        }

        private void Whiteboard_Paint(object sender, PaintEventArgs e) // Occurs when the control is redrawn
        {
            if (isDrawing && points.Count > 1) // Display a preview of the line that the user is preparing to draw
            {
                using (Pen pen = new Pen(Color.Gray, Linewidth))
                {
                    for (int i = 0; i < points.Count - 1; i++)
                    {
                        e.Graphics.DrawLine(pen, points[i], points[i + 1]);
                    }
                }
            }

            foreach (Image image in images.Keys)
            {
                // Draw image onto the Whiteboard
                e.Graphics.DrawImage(image, images[image]); // Insert image into rectangle

                // Drawing a selection outline
                if (image == selectedImage)
                {
                    using (Pen pen = new Pen(Color.Blue, 2))
                    {
                        e.Graphics.DrawRectangle(pen, images[image]);
                    }

                    // Calculate position of the resize handle
                    int handleX = images[image].Right - handleSize;
                    int handleY = images[image].Bottom - handleSize;
                    resizeHandle = new Rectangle(handleX, handleY, handleSize, handleSize);

                    // Draw resize handles
                    using (SolidBrush brush = new SolidBrush(Color.Blue))
                    {
                        // Draw handles on the corners
                        e.Graphics.FillRectangle(brush, resizeHandle);
                    }
                }
            }
        }

        private void DrawLine(List<Point> points, int Linewidth, Color penColor)
        {
            // Draws the points from the list
            using (Pen pen = new Pen(penColor, Linewidth))
            {
                for (int i = 0; i < points.Count - 1; i++)
                {
                    graphics.DrawLine(pen, points[i], points[i + 1]);
                }
            }
            Whiteboard.Invalidate(); // Triggers a repaint to show the final drawing
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AllowFullOpen = true;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                penColor = colorDialog.Color;
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (tcpClient.Connected)
            {
                if (undoStack.Count == 0)
                {
                    CloseClient();
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to save your work?", "Save Work", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Yes:
                        SaveFileDialog saveFile = new SaveFileDialog();
                        saveFile.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
                        saveFile.Title = "Save drawing";

                        if (saveFile.ShowDialog() == DialogResult.OK)
                        {
                            // Get the file name selected by the user
                            string fileName = saveFile.FileName;
                            string imageFormat = ".png"; // Default to PNG
                            if (saveFile.FilterIndex == 2) // If JPEG is selected
                            {
                                imageFormat = ".jpg";
                            }
                            SaveImage(fileName, imageFormat);
                            SendEndSignal(fileName, imageFormat);
                        }
                        CloseClient();
                        break;
                    case DialogResult.No:
                        CloseClient();
                        break;
                    case DialogResult.Cancel:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Haven't connected to server", "Error");
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string imageURL = textBox1.Text.Trim();
            try
            {
                using (HttpClient client = new HttpClient()) // Get image from the internet
                {
                    HttpResponseMessage response = await client.GetAsync(imageURL);
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        Image image = Image.FromStream(stream);
                        isAssigningImage = true;

                        // Calculate the new image size and reset its position
                        Size defaultSize = new Size(200, 200); // Desire image rectangle size
                        float aspectRatio = (float)image.Width / image.Height; // Source image aspect ratio 
                        Size newSize;
                        if (defaultSize.Width / aspectRatio > defaultSize.Height) // Scale rectangle size to match source image aspect ratio
                        {
                            newSize = new Size((int)(defaultSize.Height * aspectRatio), defaultSize.Height);
                        }
                        else
                        {
                            newSize = new Size(defaultSize.Width, (int)(defaultSize.Width / aspectRatio));
                        }
                        Point imageLocation = new Point(30, 30);
                        Rectangle imageRect = new Rectangle(imageLocation, newSize); // Creating a new Rectangle object that represents the position and size of the image 

                        images[image] = imageRect;
                        Whiteboard.Invalidate(); // Trigger a repaint to show the new image
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Add picture error: {ex.Message}", "Error");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (selectedImage != null)
            {
                images.Remove(selectedImage);
                selectedImage = null;
                Whiteboard.Invalidate();
            }
        }

        private void SendPointData(Point point, string action = "draw") // Send point to server
        {
            string data = $"{action}|{point.X},{point.Y}|{Linewidth}|{penColor.ToArgb()}"; // Include linewidth and color
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            stream.Write(dataBytes, 0, dataBytes.Length);
        }

        private void SendEndSignal(string fileName, string imageFormat)
        {
            string data = $"{fileName}|{imageFormat}";
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            stream.Write(dataBytes, 0, dataBytes.Length);
        }

        private void SendActionSignal(string action) // Send redo or undo signal
        {
            string data = action;
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            stream.Write(dataBytes, 0, dataBytes.Length);
        }

        private void ReceivedFromServer()
        {
            while (tcpClient.Connected)
            {
                try
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length); // Error: connection abort while reading the data
                    if (bytesRead > 0)
                    {
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        ProcessReceivedData(receivedData);
                    }
                }
                catch (IOException) // Unable to read data
                {
                    /*MessageBox.Show($"I/O Error: {io.Message}");*/
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void ProcessReceivedData(string data)
        {
            string[] parts = data.Split('|');

            if (parts.Length == 1) // Check action signal
            {
                if (parts[0] == "undo")
                {
                    BeginInvoke((Action)(() =>
                    {
                        Undo();
                    }));
                }
                if (parts[0] == "redo")
                {
                    BeginInvoke((Action)(() =>
                    {
                        Redo();
                    }));
                }
            }

            if (parts.Length == 2)
            {
                if (parts[0] == "Number of clients") // Check if server send the number of clients
                {
                    try
                    {
                        clientCount = Int32.Parse(parts[1]);
                        WriteTextSafe($"Number of clients: {clientCount}", groupBox2); // Error: Accessing disposed object (*Fixed)
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Update GroupBox Text Error: {ex.Message}");
                    }
                }
                else // If one client hit 'End' button => Save image
                {
                    try
                    {
                        SaveImage(parts[0], parts[1]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}", "Save image error");
                    }

                    BeginInvoke((Action)(() =>
                    {
                        CloseClient();
                    }));
                }
            }

            if (parts.Length == 4) // Check if data contains enough parts for drawing
            {
                string action = parts[0];
                string[] pointData = parts[1].Split(',');
                if (pointData.Length == 2 && int.TryParse(pointData[0], out int x) && int.TryParse(pointData[1], out int y))
                {
                    Point point = new Point(x, y);
                    if (action == "start")
                    {
                        BeginInvoke((Action)(() =>
                        {
                            isDrawing = true;
                            points.Clear();
                            points.Add(point);
                            SaveStateForUndo();
                        }));
                    }
                    else if (action == "draw")
                    {
                        BeginInvoke((Action)(() =>
                        {
                            points.Add(point);
                            int width = int.Parse(parts[2]); // Parse linewidth
                            Color color = Color.FromArgb(int.Parse(parts[3])); // Parse color
                            DrawLine(points, width, color);
                            Whiteboard.Invalidate();
                        }));
                    }
                    else if (action == "end")
                    {
                        BeginInvoke((Action)(() =>
                        {
                            isDrawing = false;
                            points.Add(point);
                            int width = int.Parse(parts[2]); // Parse linewidth
                            Color color = Color.FromArgb(int.Parse(parts[3])); // Parse color
                            DrawLine(points, width, color);
                            SaveStateForRedo();
                            points.Clear();
                        }));
                    }
                }
            }
        }

        private void CloseClient()
        {
            tcpClient.Close();
            stream.Close();
            drawingBitmap.Dispose();
            graphics.Dispose();
            this.Close();
        }

        private void Client_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                Undo();
                SendActionSignal("undo");
            }

            if (e.Control && e.KeyCode == Keys.Y)
            {
                Redo();
                SendActionSignal("redo");
            }
        }

        private void SaveStateForUndo()
        {
            Bitmap currentState = (Bitmap)drawingBitmap.Clone();
            undoStack.Push(currentState);
        }

        private void SaveStateForRedo()
        {
            Bitmap currentState = (Bitmap)drawingBitmap.Clone();
            redoStack.Push(currentState);
        }

        private void Undo()
        {
            if (undoStack.Count > 0)
            {
                SaveStateForRedo();

                drawingBitmap = undoStack.Pop();
                graphics = Graphics.FromImage(drawingBitmap);
                Whiteboard.Image = drawingBitmap;
                Whiteboard.Invalidate();
            }
        }

        private void Redo()
        {
            if (redoStack.Count > 0)
            {
                if (!BitmapsAreEqual(drawingBitmap, redoStack.Peek()))
                {
                    SaveStateForUndo();

                    drawingBitmap = redoStack.Pop();
                    graphics = Graphics.FromImage(drawingBitmap);
                    Whiteboard.Image = drawingBitmap;
                    Whiteboard.Invalidate();
                }
            }
        }

        private bool BitmapsAreEqual(Bitmap bmp1, Bitmap bmp2) // Check if 2 bitmaps are the same
        {
            if (bmp1 == null || bmp2 == null)
                return false;

            if (bmp1.Width != bmp2.Width || bmp1.Height != bmp2.Height)
                return false;

            for (int x = 0; x < bmp1.Width; x++)
            {
                for (int y = 0; y < bmp1.Height; y++)
                {
                    if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                        return false;
                }
            }

            return true;
        }

        private void SaveImage(string fileName, string imageFormat)
        {
            ImageFormat format = ImageFormat.Png; // Default to PNG
            if (imageFormat == ".jpg")
            {
                format = ImageFormat.Jpeg;
            }

            lock (saveimglock) // Ensure only 1 client (one after another) can access (save image) at a time
            {
                int i = 1;
                string withoutExtension = Path.GetFileNameWithoutExtension(fileName); // abc\image.png => output: image
                string directory = Path.GetDirectoryName(fileName) ?? string.Empty; // abcd\abc\image.png => output: abcd\abc
                while (File.Exists(fileName))
                {
                    fileName = Path.Combine(directory, $"{withoutExtension}{i:D2}{imageFormat}");
                    i++;

                }

                // Save the image with the selected format
                Whiteboard.Image.Save(fileName, format);
                MessageBox.Show($"Drawing saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Change pen line width
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Linewidth = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Linewidth = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Linewidth = 3;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Linewidth = 4;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Linewidth = 5;
        }

        private void WriteTextSafe(string text, Control control)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(new Action(() =>
                {
                    if (!control.IsDisposed && control is GroupBox gb)
                    {
                        gb.Text = text;
                    }
                }));
            }
            else
            {
                if (!control.IsDisposed && control is GroupBox gb)
                {
                    gb.Text = text;
                }
            }
        }
    }
}
