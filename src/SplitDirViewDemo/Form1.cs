using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitDirViewDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void splitDirView1_FolderChanged(object sender, EventArgs e)
        {
            label1.Text = splitDirView1.currentFolder;
        }

        private void splitDirView1_FileSelected(object sender, EventArgs e)
        {
            label2.Text = splitDirView1.highlightedFile;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            splitDirView1.SetFolder("C:/Windows/");
            splitDirView1.SetMatchingBackColor(".exe", Color.Yellow);
        }
    }
}
