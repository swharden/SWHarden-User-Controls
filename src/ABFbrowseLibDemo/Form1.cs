using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABFbrowseLib;

namespace ABFbrowseLibTest
{
    public partial class Form1 : Form
    {

        public ABFfolder abfFolder;

        public Form1()
        {
            InitializeComponent();

            //string startupFolder = @"D:\abfData\abfs-real";
            string startupFolder = @"D:\abfData\abfs-real";
            if (System.IO.Directory.Exists(startupFolder))
            {
                abfFolder = new ABFfolder(startupFolder);
                dataGridView1.DataSource = abfFolder.GetDataTable(cbParentsOnly.Checked);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var diag = new FolderBrowserDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                abfFolder = new ABFfolder(diag.SelectedPath);
                dataGridView1.DataSource = abfFolder.GetDataTable(cbParentsOnly.Checked);
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int thisRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                string abfFilePath = dataGridView1.Rows[thisRow].Cells[1].Value.ToString();
                string abfFileName = System.IO.Path.GetFileName(abfFilePath);

                dataGridView1.ClearSelection();
                dataGridView1.Rows[thisRow].Selected = true;

                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem($"Copy Path to {abfFileName}"));
                m.MenuItems.Add(new MenuItem($"Launch {abfFileName} in ClampFit"));
                m.Show(dataGridView1, new Point(e.X, e.Y));

            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // this runs after a sort, so it's a good time to re-style things

            // color based on file size
            for (int row = 0; row < dataGridView1.Rows.Count; row++)
            {
                string cellValue = dataGridView1.Rows[row].Cells[9].Value.ToString();
                if (Convert.ToDouble(cellValue) > 1)
                {
                    dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.LightPink;
                } else
                {
                    dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }
    }
}
