namespace Lab3_Bai3
{
    partial class Client
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
            btnSend = new Button();
            richTextBox1 = new RichTextBox();
            btnConnect = new Button();
            btnQuit = new Button();
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.Location = new Point(12, 52);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(270, 59);
            btnSend.TabIndex = 0;
            btnSend.Text = "Send message";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(288, 52);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(500, 362);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(12, 127);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(270, 59);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnQuit
            // 
            btnQuit.Location = new Point(12, 201);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(270, 59);
            btnQuit.TabIndex = 3;
            btnQuit.Text = "Quit";
            btnQuit.UseVisualStyleBackColor = true;
            btnQuit.Click += btnQuit_Click;
            // 
            // Client
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnQuit);
            Controls.Add(btnConnect);
            Controls.Add(richTextBox1);
            Controls.Add(btnSend);
            Name = "Client";
            Text = "Client";
            ResumeLayout(false);
        }

        #endregion

        private Button btnSend;
        private RichTextBox richTextBox1;
        private Button btnConnect;
        private Button btnQuit;
    }
}