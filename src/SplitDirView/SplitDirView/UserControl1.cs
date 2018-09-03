using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitDirView
{
    public partial class UserControl1: UserControl
    {

        private NavFolders navFolders;
        private NavFiles navFiles;

        public UserControl1()
        {
            InitializeComponent();
            NavFolderSet(@"C:\Users\scott\Documents\GitHub\pyABF\data\abfs");
        }

        public event EventHandler FolderChanged;
        protected virtual void OnFolderChanged(EventArgs e)
        {
            var handler = FolderChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler FileSelected;
        protected virtual void OnFileSelected(EventArgs e)
        {
            var handler = FileSelected;
            if (handler != null)
                handler(this, e);
        }

        public ListViewItem[] LabelsToListViewItems(string[] strings, int ImageIndex=0)
        {
            ListViewItem[] lvItems = new ListViewItem[strings.Length];
            for (int i=0; i<strings.Length; i++)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.ImageIndex = ImageIndex;
                if (strings[i].EndsWith("\\") || strings[i].EndsWith("/"))
                    lvItem.ImageIndex = 0;
                lvItem.Text = strings[i];
                //lvItem.ForeColor = Color.Blue;
                lvItems[i] = lvItem;
            }
            return lvItems;
        }

        public string currentFolder;
        private void NavFolderSet(string folder)
        {
            navFolders = new NavFolders(folder);
            lvFolders.Items.Clear();
            lvFolders.Items.AddRange(LabelsToListViewItems(navFolders.GetLabels()));

            // after populating the folder list, select the last folder
            lvFolders.Items[lvFolders.Items.Count - 1].Selected = true;
            lvFolders.Select();

            currentFolder = folder;
            OnFolderChanged(EventArgs.Empty);
        }

        private void btnSetFolder_Click(object sender, EventArgs e)
        {
            //NavFolderSet(@"C:\Users\scott\Documents\GitHub\pyABF\data\abfs");
        }

        private void lvFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("some long message here", "WARNING!!!");

            // figure what folder was clicked
            if (lvFolders.SelectedIndices == null || lvFolders.SelectedIndices.Count == 0)
                return;
            string path = navFolders.folders[lvFolders.SelectedIndices[0]].path;

            // populate the file list
            navFiles = new NavFiles(path);
            lvFiles.Items.Clear();
            lvFiles.Items.AddRange(LabelsToListViewItems(navFiles.GetPathNames(), 2));
            lvFiles.Columns[0].Width = -2;
        }

        private void lvFolders_DoubleClick(object sender, EventArgs e)
        {
            // figure what folder was clicked
            if (lvFolders.SelectedIndices == null || lvFolders.SelectedIndices.Count == 0)
                return;
            string path = navFolders.folders[lvFolders.SelectedIndices[0]].path;
            NavFolderSet(path);
        }

        public string highlightedFile;
        private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            // figure what folder was clicked
            if (lvFiles.SelectedIndices == null || lvFiles.SelectedIndices.Count == 0)
                return;
            string path = navFiles.paths[lvFiles.SelectedIndices[0]];
            highlightedFile = path;
        }

        public string doubleClickedFile;
        private void lvFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // figure what folder was clicked
            if (lvFiles.SelectedIndices == null || lvFiles.SelectedIndices.Count == 0)
                return;
            string path = navFiles.paths[lvFiles.SelectedIndices[0]];

            // determine what to do if it's a file or folder
            if (System.IO.Directory.Exists(path))
            {
                System.Console.WriteLine($"double-clicked a DIRECTORY: {path}");
                NavFolderSet(path);
            }
            else
            {
                System.Console.WriteLine($"double-clicked a FILE: {path}");
                doubleClickedFile = path;
                OnFileSelected(EventArgs.Empty);
            }
        }

        private void btnCopyFolder_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(currentFolder);
        }

        private void btnCopyFile_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(highlightedFile);
        }

        private void btnLaunchFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"{currentFolder}");
        }

        private void btnLaunchFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"{highlightedFile}");
        }
    }
}
