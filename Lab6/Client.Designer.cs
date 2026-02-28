namespace Lab6
{
    partial class Client
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnEnd = new Button();
            btnColor = new Button();
            groupBox1 = new GroupBox();
            radioButton5 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton1 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            label1 = new Label();
            groupBox2 = new GroupBox();
            Whiteboard = new PictureBox();
            textBox1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            btnAdd = new Button();
            btnRemove = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Whiteboard).BeginInit();
            SuspendLayout();
            // 
            // btnEnd
            // 
            btnEnd.Location = new Point(12, 410);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(130, 50);
            btnEnd.TabIndex = 1;
            btnEnd.Text = "END";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // btnColor
            // 
            btnColor.Location = new Point(148, 410);
            btnColor.Name = "btnColor";
            btnColor.Size = new Size(130, 50);
            btnColor.TabIndex = 2;
            btnColor.Text = "COLOR";
            btnColor.UseVisualStyleBackColor = true;
            btnColor.Click += btnColor_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton5);
            groupBox1.Controls.Add(radioButton4);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Location = new Point(644, 399);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(226, 54);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Width";
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(184, 24);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(38, 24);
            radioButton5.TabIndex = 14;
            radioButton5.TabStop = true;
            radioButton5.Text = "5";
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.CheckedChanged += radioButton5_CheckedChanged;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(140, 24);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(38, 24);
            radioButton4.TabIndex = 13;
            radioButton4.TabStop = true;
            radioButton4.Text = "4";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += radioButton4_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(8, 24);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(38, 24);
            radioButton1.TabIndex = 10;
            radioButton1.TabStop = true;
            radioButton1.Text = "1";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(96, 24);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(38, 24);
            radioButton3.TabIndex = 12;
            radioButton3.TabStop = true;
            radioButton3.Text = "3";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(52, 24);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(38, 24);
            radioButton2.TabIndex = 11;
            radioButton2.TabStop = true;
            radioButton2.Text = "2";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 483);
            label1.Name = "label1";
            label1.Size = new Size(89, 20);
            label1.TabIndex = 10;
            label1.Text = "Coordinates";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(Whiteboard);
            groupBox2.Location = new Point(12, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(858, 393);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Number of clients:";
            // 
            // Whiteboard
            // 
            Whiteboard.Location = new Point(6, 26);
            Whiteboard.Name = "Whiteboard";
            Whiteboard.Size = new Size(846, 361);
            Whiteboard.TabIndex = 15;
            Whiteboard.TabStop = false;
            Whiteboard.Paint += Whiteboard_Paint;
            Whiteboard.MouseDown += Whiteboard_MouseDown;
            Whiteboard.MouseMove += Whiteboard_MouseMove;
            Whiteboard.MouseUp += Whiteboard_MouseUp;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(335, 422);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(285, 27);
            textBox1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(298, 399);
            label2.Name = "label2";
            label2.Size = new Size(86, 20);
            label2.TabIndex = 13;
            label2.Text = "Add image:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(298, 427);
            label3.Name = "label3";
            label3.Size = new Size(31, 20);
            label3.TabIndex = 14;
            label3.Text = "Url:";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(335, 455);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 15;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(435, 455);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(94, 29);
            btnRemove.TabIndex = 16;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // Client
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 503);
            Controls.Add(btnRemove);
            Controls.Add(btnAdd);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(groupBox2);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Controls.Add(btnColor);
            Controls.Add(btnEnd);
            Name = "Client";
            Text = "Whiteboard";
            KeyDown += Client_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Whiteboard).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnEnd;
        private Button btnColor;
        private GroupBox groupBox1;
        private RadioButton radioButton5;
        private RadioButton radioButton4;
        private RadioButton radioButton1;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private Label label1;
        private GroupBox groupBox2;
        private TextBox textBox1;
        private Label label2;
        private Label label3;
        private PictureBox Whiteboard;
        private Button btnAdd;
        private Button btnRemove;
    }
}
