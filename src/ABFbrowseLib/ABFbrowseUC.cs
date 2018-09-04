using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABFbrowseLib
{
    public partial class ABFbrowseUC : UserControl
    {
        public ABFfolder abfFolder;
        public bool showParentsOnly = false;

        public ABFbrowseUC()
        {
            InitializeComponent();
        }

        public void SetFolder(string folder)
        {
            abfFolder = new ABFfolder(folder);
            dataGridView1.DataSource = abfFolder.GetDataTable(showParentsOnly);
        }

        public event EventHandler ABFclicked;
        protected virtual void OnABFclicked(EventArgs e)
        {
            var handler = ABFclicked;
            if (handler != null)
                handler(this, e);
        }

        public string selectedABF;
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

            // figure out what ABF was clicked
            int thisRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            string abfFilePath = dataGridView1.Rows[thisRow].Cells[2].Value.ToString();
            string abfFileName = System.IO.Path.GetFileName(abfFilePath);
            selectedABF = abfFilePath;
            OnABFclicked(EventArgs.Empty);

            if (e.Button == MouseButtons.Right)
            {

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
                }
                else
                {
                    dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
