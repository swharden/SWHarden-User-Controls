using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IconGalleryDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var diag = new FolderBrowserDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                iconGallery1.SetFolder(diag.SelectedPath);
            }
        }

        private void iconGallery1_IconSelected(object sender, EventArgs e)
        {
            label1.Text = iconGallery1.iconSelected;
            label1.ForeColor = Color.Blue;
        }

        private void iconGallery1_IconDoubleClicked(object sender, EventArgs e)
        {
            label1.Text = iconGallery1.iconSelected;
            label1.ForeColor = Color.Red;
        }
    }
}
