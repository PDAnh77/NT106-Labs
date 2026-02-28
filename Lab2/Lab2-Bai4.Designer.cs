namespace Lab2
{
    partial class Form5
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
            button3 = new Button();
            WriteButton = new Button();
            ReadButton = new Button();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(287, 31);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(501, 407);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.Location = new Point(12, 206);
            button3.Name = "button3";
            button3.Size = new Size(225, 68);
            button3.TabIndex = 8;
            button3.Text = "EXIT";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // WriteButton
            // 
            WriteButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            WriteButton.Location = new Point(12, 118);
            WriteButton.Name = "WriteButton";
            WriteButton.Size = new Size(225, 68);
            WriteButton.TabIndex = 7;
            WriteButton.Text = "READ";
            WriteButton.UseVisualStyleBackColor = true;
            WriteButton.Click += WriteButton_Click;
            // 
            // ReadButton
            // 
            ReadButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ReadButton.Location = new Point(12, 31);
            ReadButton.Name = "ReadButton";
            ReadButton.Size = new Size(225, 68);
            ReadButton.TabIndex = 6;
            ReadButton.Text = "SAVE";
            ReadButton.UseVisualStyleBackColor = true;
            ReadButton.Click += ReadButton_Click;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button3);
            Controls.Add(WriteButton);
            Controls.Add(ReadButton);
            Controls.Add(richTextBox1);
            Name = "Form5";
            Text = "Bài 4";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox1;
        private Button button3;
        private Button WriteButton;
        private Button ReadButton;
    }
}