namespace Lab2
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBox1 = new RichTextBox();
            label1 = new Label();
            Read_Button = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            fileName = new TextBox();
            fileUrl = new TextBox();
            linesOut = new TextBox();
            wordsOut = new TextBox();
            charactesOut = new TextBox();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(318, 37);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(432, 379);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 137);
            label1.Name = "label1";
            label1.Size = new Size(57, 20);
            label1.TabIndex = 2;
            label1.Text = "Tên file";
            // 
            // Read_Button
            // 
            Read_Button.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Read_Button.Location = new Point(44, 37);
            Read_Button.Name = "Read_Button";
            Read_Button.Size = new Size(225, 68);
            Read_Button.TabIndex = 3;
            Read_Button.Text = "ĐỌC FILE";
            Read_Button.UseVisualStyleBackColor = true;
            Read_Button.Click += Read_Button_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 177);
            label2.Name = "label2";
            label2.Size = new Size(28, 20);
            label2.TabIndex = 4;
            label2.Text = "Url";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 217);
            label3.Name = "label3";
            label3.Size = new Size(65, 20);
            label3.TabIndex = 5;
            label3.Text = "Số dòng";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 257);
            label4.Name = "label4";
            label4.Size = new Size(44, 20);
            label4.TabIndex = 6;
            label4.Text = "Số từ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 297);
            label5.Name = "label5";
            label5.Size = new Size(62, 20);
            label5.TabIndex = 7;
            label5.Text = "Số ký tự";
            // 
            // fileName
            // 
            fileName.Location = new Point(88, 134);
            fileName.Name = "fileName";
            fileName.Size = new Size(211, 27);
            fileName.TabIndex = 8;
            // 
            // fileUrl
            // 
            fileUrl.Location = new Point(88, 174);
            fileUrl.Name = "fileUrl";
            fileUrl.Size = new Size(211, 27);
            fileUrl.TabIndex = 9;
            // 
            // linesOut
            // 
            linesOut.Location = new Point(88, 214);
            linesOut.Name = "linesOut";
            linesOut.Size = new Size(211, 27);
            linesOut.TabIndex = 10;
            // 
            // wordsOut
            // 
            wordsOut.AcceptsReturn = true;
            wordsOut.Location = new Point(88, 254);
            wordsOut.Name = "wordsOut";
            wordsOut.Size = new Size(211, 27);
            wordsOut.TabIndex = 11;
            // 
            // charactesOut
            // 
            charactesOut.Location = new Point(88, 294);
            charactesOut.Name = "charactesOut";
            charactesOut.Size = new Size(211, 27);
            charactesOut.TabIndex = 12;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(charactesOut);
            Controls.Add(wordsOut);
            Controls.Add(linesOut);
            Controls.Add(fileUrl);
            Controls.Add(fileName);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(Read_Button);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Name = "Form3";
            Text = "Bài 2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RichTextBox richTextBox1;
        private Label label1;
        private Button Read_Button;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox fileName;
        private TextBox fileUrl;
        private TextBox linesOut;
        private TextBox wordsOut;
        private TextBox charactesOut;
    }
}