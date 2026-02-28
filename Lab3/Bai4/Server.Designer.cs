namespace Lab3_Bai4
{
    partial class Server
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
            btnListen = new Button();
            listView1 = new ListView();
            btnQuit = new Button();
            SuspendLayout();
            // 
            // btnListen
            // 
            btnListen.Location = new Point(450, 29);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(154, 39);
            btnListen.TabIndex = 0;
            btnListen.Text = "Listen";
            btnListen.UseVisualStyleBackColor = true;
            btnListen.Click += btnListen_Click;
            // 
            // listView1
            // 
            listView1.Location = new Point(34, 74);
            listView1.Name = "listView1";
            listView1.Size = new Size(730, 339);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.List;
            // 
            // btnQuit
            // 
            btnQuit.Location = new Point(610, 29);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(154, 39);
            btnQuit.TabIndex = 2;
            btnQuit.Text = "Quit";
            btnQuit.UseVisualStyleBackColor = true;
            btnQuit.Click += btnQuit_Click;
            // 
            // Server
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnQuit);
            Controls.Add(listView1);
            Controls.Add(btnListen);
            Name = "Server";
            Text = "Server";
            ResumeLayout(false);
        }

        #endregion

        private Button btnListen;
        private ListView listView1;
        private Button btnQuit;
    }
}