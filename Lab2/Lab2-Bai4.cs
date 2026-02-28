using Lab2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        FileStream fs;
        
        string output = "C:\\Users\\anhph\\Desktop\\BT Code\\Lập trình mạng căn bản\\Thực hành\\Lab2\\output.txt";

        private void ReadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string filepath = ofd.FileName;

            // Create an array of students
            List<Student> students = new List<Student>();
            // Read text from RichTextBox
            string inputText = richTextBox1.Text;

            // Split the text into lines
            string[] lines = inputText.Split(new[] { "\r\n\r\n", "\r\r", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            int count = 0;
            // Process each line to create Student objects
            foreach (var line in lines)
            {
                // Split the line into parts
                string[] parts = line.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

                // Validate and create a Student object
                if (parts.Length == 5) // Ensure all required fields are present
                {
                    string mssv = parts[0];
                    string hoten = parts[1];
                    string dienthoai = parts[2];
                    float diemtoan, diemvan;

                    if (float.TryParse(parts[3], out diemtoan) && float.TryParse(parts[4], out diemvan))
                    {
                        Student student = new Student(mssv, hoten, dienthoai, diemtoan, diemvan);
                        students.Add(student);
                    }
                    else
                    {
                        MessageBox.Show("Điểm không hợp lệ!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Thông tin không hợp lệ!");
                    return;
                }

                // Write students data to input.txt
                try
                {
                    WriteStudentsToFile(students, filepath);
                    count++;
                    MessageBox.Show($"Thêm thông tin sinh viên {count} thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi");
                }
            }
            SetOutputFile(filepath);
        }

        private void WriteButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string filepath = ofd.FileName;

            // Read student data from output.txt and display
            List<Student> students = ReadStudentsFromFile(filepath);

            if (students != null)
            {
                richTextBox1.Clear();
                foreach (var student in students)
                {
                    richTextBox1.AppendText($"MSSV: {student.Mssv}\n");
                    richTextBox1.AppendText($"Họ và tên: {student.Hoten}\n");
                    richTextBox1.AppendText($"Số điện thoại: {student.Dienthoai}\n");
                    richTextBox1.AppendText($"Điểm toán: {student.Diemtoan}\n");
                    richTextBox1.AppendText($"Điểm văn: {student.Diemvan}\n");
                    richTextBox1.AppendText($"Điểm trung bình: {student.DTB}\n\n");
                }
            }
            else
            {
                MessageBox.Show("Không thể đọc thông tin sinh viên!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WriteStudentsToFile(List<Student> students, string fileName)
        {
            try
            {
                // Open a FileStream for writing
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
                {
                    // Create a BinaryWriter to write binary data to the file
                    using (BinaryWriter writer = new BinaryWriter(fileStream))
                    {
                        // Write number of students
                        writer.Write(students.Count);
                        // Write each student's data
                        foreach (var student in students)
                        {
                            writer.Write(student.Mssv);
                            writer.Write(student.Hoten);
                            writer.Write(student.Dienthoai);
                            writer.Write(student.Diemtoan);
                            writer.Write(student.Diemvan);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private List<Student> ReadStudentsFromFile(string fileName)
        {
            List<Student> students = new List<Student>();
            try
            {
                // Open a FileStream for reading
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
                {

                    // Create a BinaryReader to read binary data from the file
                    using (BinaryReader reader = new BinaryReader(fileStream))
                    {
                        // Read number of students
                        int count = reader.ReadInt32();
                        // Read each student's data
                        for (int i = 0; i < count; i++)
                        {
                            string mssv = reader.ReadString();
                            string hoten = reader.ReadString();
                            string dienthoai = reader.ReadString();
                            float diemtoan = reader.ReadSingle();
                            float diemvan = reader.ReadSingle();
                            students.Add(new Student(mssv, hoten, dienthoai, diemtoan, diemvan));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
                return null;
            }
            return students;
        }
        private void SetOutputFile(string fileName)
        {
            try
            {
                List<Student> students = ReadStudentsFromFile(fileName);
                WriteStudentsToFile(students, output);
                MessageBox.Show("Ghi danh sách sinh viên từ input.txt vào output.txt thành công!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }
    }
}


//Serialized and Deserialized

/*FileStream fs;

string output = "C:\\Users\\ADMIN\\Desktop\\Lập trình mạng\\THanh\\Lab2\\output_Bai4.txt";

private void ReadButton_Click(object sender, EventArgs e)
{
    OpenFileDialog ofd = new OpenFileDialog();
    ofd.Filter = "Text Files|*.txt";
    ofd.FilterIndex = 1;
    ofd.ShowDialog();
    string filepath = ofd.FileName;

    // Create an array of students
    List<Student> students = new List<Student>();
    // Read text from RichTextBox
    string inputText = richTextBox1.Text;

    // Split the text into lines
    string[] lines = inputText.Split(new[] { "\r\n\r\n", "\r\r", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

    int count = 0;
    // Process each line to create Student objects
    foreach (var line in lines)
    {
        // Split the line into parts
        string[] parts = line.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

        // Validate and create a Student object
        if (parts.Length == 5) // Ensure all required fields are present
        {
            string mssv = parts[0];
            string hoten = parts[1];
            string dienthoai = parts[2];
            float diemtoan, diemvan;

            if (float.TryParse(parts[3], out diemtoan) && float.TryParse(parts[4], out diemvan))
            {
                Student student = new Student(mssv, hoten, dienthoai, diemtoan, diemvan);
                students.Add(student);
            }
            else
            {
                MessageBox.Show("Điểm không hợp lệ!");
                return;
            }
        }
        else
        {
            MessageBox.Show("Thông tin không hợp lệ!");
            return;
        }

        // Write students data to input.txt
        try
        {
            WriteStudentsToFile(students, filepath);
            count++;
            MessageBox.Show($"Thêm thông tin sinh viên {count} thành công!");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi");
        }
    }
    SetOutputFile(filepath);
}

private void WriteButton_Click(object sender, EventArgs e)
{
    OpenFileDialog ofd = new OpenFileDialog();
    ofd.ShowDialog();
    string filepath = ofd.FileName;

    // Read student data from output.txt and display
    List<Student> students = ReadStudentsFromFile(filepath);

    if (students != null)
    {
        richTextBox1.Clear();
        foreach (var student in students)
        {
            richTextBox1.AppendText($"MSSV: {student.Mssv}\n");
            richTextBox1.AppendText($"Họ và tên: {student.Hoten}\n");
            richTextBox1.AppendText($"Số điện thoại: {student.Dienthoai}\n");
            richTextBox1.AppendText($"Điểm toán: {student.Diemtoan}\n");
            richTextBox1.AppendText($"Điểm văn: {student.Diemvan}\n");
            richTextBox1.AppendText($"Điểm trung bình: {student.DTB}\n\n");
        }
    }
    else
    {
        MessageBox.Show("Không thể đọc thông tin sinh viên!");
    }
}

*//*private void button3_Click(object sender, EventArgs e)
{
    this.Close();
}

private void WriteStudentsToFile(List<Student> students, string fileName)
{
    try
    {
        // Open a FileStream for writing
        using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
        {
            // Create a BinaryFormatter to serialize the students list
            BinaryFormatter formatter = new BinaryFormatter();
            // Serialize the students list to the file
            formatter.Serialize(fileStream, students);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Lỗi: {ex.Message}");
    }
}

private List<Student> ReadStudentsFromFile(string fileName)
{
    try
    {
        // Open a FileStream for reading
        using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
        {
            // Create a BinaryFormatter to deserialize the students list
            BinaryFormatter formatter = new BinaryFormatter();
            // Deserialize the students list from the file
            List<Student> students = (List<Student>)formatter.Deserialize(fileStream);
            return students;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Lỗi: {ex.Message}");
        return null;
    }
}

private void SetOutputFile(string filepath)
{
    List<Student> students = ReadStudentsFromFile(filepath);
    WriteStudentsToFile(students, output);

    MessageBox.Show($"Đã lưu kết quả vào file {output}");
}*/