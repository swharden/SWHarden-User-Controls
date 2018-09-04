using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABFbrowseLibTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            abFbrowseUC1.SetFolder(@"D:\abfData\abfs-real");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var diag = new FolderBrowserDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = diag.SelectedPath;
                abFbrowseUC1.SetFolder(selectedPath);
            }
        }

        private void abFbrowseUC1_ABFclicked_1(object sender, EventArgs e)
        {
            label1.Text = abFbrowseUC1.selectedABF;
        }
    }
}
