namespace SplitDirView
{
    partial class UserControl1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("C:\\");
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(" test");
            System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem("  folder");
            System.Windows.Forms.ListViewItem listViewItem22 = new System.Windows.Forms.ListViewItem("   path");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl1));
            System.Windows.Forms.ListViewItem listViewItem23 = new System.Windows.Forms.ListViewItem("folder", "Folder_16x.png");
            System.Windows.Forms.ListViewItem listViewItem24 = new System.Windows.Forms.ListViewItem("file", "RFile_16x.png");
            this.btnSetFolder = new System.Windows.Forms.Button();
            this.lvFolders = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lvFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCopyFolder = new System.Windows.Forms.Button();
            this.btnLaunchFolder = new System.Windows.Forms.Button();
            this.btnCopyFile = new System.Windows.Forms.Button();
            this.btnLaunchFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSetFolder
            // 
            this.btnSetFolder.Location = new System.Drawing.Point(27, 10);
            this.btnSetFolder.Name = "btnSetFolder";
            this.btnSetFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSetFolder.TabIndex = 2;
            this.btnSetFolder.Text = "Set Folder";
            this.btnSetFolder.UseVisualStyleBackColor = true;
            this.btnSetFolder.Click += new System.EventHandler(this.btnSetFolder_Click);
            // 
            // lvFolders
            // 
            this.lvFolders.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvFolders.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvFolders.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem15,
            listViewItem16,
            listViewItem21,
            listViewItem22});
            this.lvFolders.Location = new System.Drawing.Point(27, 39);
            this.lvFolders.Name = "lvFolders";
            this.lvFolders.Size = new System.Drawing.Size(285, 227);
            this.lvFolders.TabIndex = 3;
            this.lvFolders.UseCompatibleStateImageBehavior = false;
            this.lvFolders.View = System.Windows.Forms.View.List;
            this.lvFolders.SelectedIndexChanged += new System.EventHandler(this.lvFolders_SelectedIndexChanged);
            this.lvFolders.DoubleClick += new System.EventHandler(this.lvFolders_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Folder_16x.png");
            this.imageList1.Images.SetKeyName(1, "FolderOpen_16x.png");
            this.imageList1.Images.SetKeyName(2, "RFile_16x.png");
            // 
            // lvFiles
            // 
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvFiles.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvFiles.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem23,
            listViewItem24});
            this.lvFiles.Location = new System.Drawing.Point(27, 272);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(285, 397);
            this.lvFiles.SmallImageList = this.imageList1;
            this.lvFiles.TabIndex = 4;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.SelectedIndexChanged += new System.EventHandler(this.lvFiles_SelectedIndexChanged);
            this.lvFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFiles_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 123;
            // 
            // btnCopyFolder
            // 
            this.btnCopyFolder.Location = new System.Drawing.Point(108, 10);
            this.btnCopyFolder.Name = "btnCopyFolder";
            this.btnCopyFolder.Size = new System.Drawing.Size(75, 23);
            this.btnCopyFolder.TabIndex = 5;
            this.btnCopyFolder.Text = "Copy";
            this.btnCopyFolder.UseVisualStyleBackColor = true;
            this.btnCopyFolder.Click += new System.EventHandler(this.btnCopyFolder_Click);
            // 
            // btnLaunchFolder
            // 
            this.btnLaunchFolder.Location = new System.Drawing.Point(189, 10);
            this.btnLaunchFolder.Name = "btnLaunchFolder";
            this.btnLaunchFolder.Size = new System.Drawing.Size(75, 23);
            this.btnLaunchFolder.TabIndex = 6;
            this.btnLaunchFolder.Text = "Launch Folder";
            this.btnLaunchFolder.UseVisualStyleBackColor = true;
            this.btnLaunchFolder.Click += new System.EventHandler(this.btnLaunchFolder_Click);
            // 
            // btnCopyFile
            // 
            this.btnCopyFile.Location = new System.Drawing.Point(27, 675);
            this.btnCopyFile.Name = "btnCopyFile";
            this.btnCopyFile.Size = new System.Drawing.Size(75, 23);
            this.btnCopyFile.TabIndex = 7;
            this.btnCopyFile.Text = "Copy";
            this.btnCopyFile.UseVisualStyleBackColor = true;
            this.btnCopyFile.Click += new System.EventHandler(this.btnCopyFile_Click);
            // 
            // btnLaunchFile
            // 
            this.btnLaunchFile.Location = new System.Drawing.Point(108, 675);
            this.btnLaunchFile.Name = "btnLaunchFile";
            this.btnLaunchFile.Size = new System.Drawing.Size(75, 23);
            this.btnLaunchFile.TabIndex = 8;
            this.btnLaunchFile.Text = "Launch Folder";
            this.btnLaunchFile.UseVisualStyleBackColor = true;
            this.btnLaunchFile.Click += new System.EventHandler(this.btnLaunchFile_Click);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLaunchFile);
            this.Controls.Add(this.btnCopyFile);
            this.Controls.Add(this.btnLaunchFolder);
            this.Controls.Add(this.btnCopyFolder);
            this.Controls.Add(this.lvFiles);
            this.Controls.Add(this.lvFolders);
            this.Controls.Add(this.btnSetFolder);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(362, 712);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSetFolder;
        private System.Windows.Forms.ListView lvFolders;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnCopyFolder;
        private System.Windows.Forms.Button btnLaunchFolder;
        private System.Windows.Forms.Button btnCopyFile;
        private System.Windows.Forms.Button btnLaunchFile;
    }
}
