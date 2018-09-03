using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace IconGallery
{
    public partial class IconGallery: UserControl
    {
        public IconGallery()
        {
            InitializeComponent();
            //SetFolder(@"C:\Users\scott\Documents\GitHub\pyABF\data\headers");
        }

        public event EventHandler IconSelected;
        protected virtual void OnIconSelected(EventArgs e)
        {
            var handler = IconSelected;
            if (handler != null)
                handler(this, e);
        }

        public string iconSelected;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices == null || listView1.SelectedIndices.Count == 0)
            {
                iconSelected = "";
            } else
            {
                iconSelected = imagePaths[listView1.SelectedIndices[0]];
            }
            OnIconSelected(EventArgs.Empty);
        }

        public event EventHandler IconDoubleClicked;
        protected virtual void OnIconDoubleClicked(EventArgs e)
        {
            var handler = IconDoubleClicked;
            if (handler != null)
                handler(this, e);
        }

        public string iconDoubleClicked;
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices == null || listView1.SelectedIndices.Count == 0)
            {
                iconDoubleClicked = "";
            }
            else
            {
                iconDoubleClicked = imagePaths[listView1.SelectedIndices[0]];
            }
            OnIconDoubleClicked(EventArgs.Empty);
        }

        /// <summary>
        /// return a list of full paths to displayable images in a folder
        /// </summary>
        private string[] ImageFilesInFolder(string folder)
        {
            string[] imageExtensions = new string[] { ".bmp", ".jpg", ".png" };
            List<string> images = new List<string>();
            foreach (string fname in System.IO.Directory.GetFiles(folder))
            {
                string extension = Path.GetExtension(fname).ToLower();
                if (imageExtensions.Contains(extension))
                {
                    images.Add(fname);
                }
            }
            return images.ToArray();
        }

        public string[] imagePaths;
        public void SetFolder(string folder)
        {
            // clear the old contents
            listView1.Items.Clear();

            // prepare the new image list
            ImageList imageList = new ImageList();
            int imageScale = 20;
            imageList.ImageSize = new Size(8 * imageScale, 6 * imageScale);
            listView1.LargeImageList = imageList;

            // load the image files
            imagePaths = ImageFilesInFolder(folder);
            foreach (string imagePath in imagePaths)
            {
                Image image = Image.FromFile(imagePath);
                imageList.Images.Add(image);

                ListViewItem item = new ListViewItem();
                item.Text = Path.GetFileNameWithoutExtension(imagePath);
                item.Text = item.Text.Replace("_", " ").Replace("-", " ");
                item.ImageIndex = imageList.Images.Count - 1;
                listView1.Items.Add(item);
            }

        }

        private void MenuButtonCopy(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, iconSelected);
        }

        private void MenuButtonLaunch(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"{iconSelected}");
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                if (listView1.FocusedItem.Bounds.Contains(e.Location))
                {
                    string selectedItemText = listView1.FocusedItem.Text;
                    int selectedItemIndex = listView1.FocusedItem.Index;
                    iconSelected = imagePaths[selectedItemIndex];

                    ContextMenu cm = new ContextMenu();

                    MenuItem mnuCopy = new MenuItem("Copy");
                    mnuCopy.Click += new EventHandler(MenuButtonCopy);
                    cm.MenuItems.Add(mnuCopy);

                    MenuItem mnuLaunch = new MenuItem("Launch");
                    mnuLaunch.Click += new EventHandler(MenuButtonLaunch);
                    cm.MenuItems.Add(mnuLaunch);

                    cm.Show(listView1, e.Location);
                } 
            }
        }
    }
}
