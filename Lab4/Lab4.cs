using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Lab4 : Form
    {
        public Lab4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bai1 bai01 = new Bai1();
            bai01.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bai2 bai02 = new Bai2();
            bai02.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bai3 bai03 = new Bai3();
            bai03.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bai4 bai04 = new Bai4();
            bai04.Show();
        }
    }
}
