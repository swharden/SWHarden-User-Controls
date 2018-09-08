using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TifLib;

namespace TifLibDemo
{
    public partial class Form1 : Form
    {
        public TifFile tif;
        public Form1()
        {
            InitializeComponent();
            tif = new TifFile("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = tif.Info();
        }
    }
}
