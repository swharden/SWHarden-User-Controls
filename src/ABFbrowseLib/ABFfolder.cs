using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace ABFbrowseLib
{
    public class ABFfolder
    {
        public string folderPath { get; }
        public string[] folderContents;
        public List<ABFinfo> abfs;

        public const string dataFoldername = "swhlab";
        public string dataFolderPath;
        public string[] dataFolderContents;
        public string[] dataImages;

        /// <summary>
        /// The ABFfolder class groups ABF files and data from a single folder
        /// </summary>
        /// <param name="abfFolderPath"></param>
        public ABFfolder(string abfFolderPath)
        {
            folderPath = Path.GetFullPath(abfFolderPath);
            dataFolderPath = Path.Combine(folderPath, dataFoldername);
            ScanFolders();
        }

        /// <summary>
        /// Return information about the ABFfolder object
        /// </summary>
        public string Info()
        {
            string txt = "";
            txt += $"ABF Folder: {folderPath} (has {folderContents.Length} files, {abfs.Count} ABFs)\n";
            txt += $"Data Folder: {dataFolderPath} (has {dataImages.Length} images)\n";
            txt += "\n### ABFS ###\n";
            foreach (ABFinfo abf in abfs)
            {
                txt += $"{abf.parent} {abf.abfID}\n";
            }
            return txt;
        }

        /// <summary>
        /// return a list of all folders and files in a given folder.
        /// </summary>
        private string[] GetFolderContents(string path)
        {
            
            if (!Directory.Exists(path))
            {
                return new string[] { };
            }

            List<string> folders = new List<string>();
            foreach (string folder in Directory.GetDirectories(path))
            {
                folders.Add(folder + "/");
            }
            folders.Sort();

            List<string> files = new List<string>();
            foreach (string file in Directory.GetFiles(path))
            {
                files.Add(file);
            }
            files.Sort();

            List<string> allItems = new List<string>();
            allItems.AddRange(folders);
            allItems.AddRange(files);
            return allItems.ToArray();
        }
        
        /// <summary>
        /// Return the list of filenames (without extension) of all ABF or non-ABF files in the folder.
        /// This is useful for matching-up with ABFIDs to determine parents.
        /// </summary>
        private string[] GetFileIDs(bool abfs=true)
        {
            List<string> files = new List<string>();
            foreach (string fname in folderContents)
            {
                if (abfs == true && Path.GetExtension(fname).ToLower() == ".abf")
                {
                    files.Add(Path.GetFileNameWithoutExtension(fname));
                }
                else if (abfs == false && Path.GetExtension(fname).ToLower() != ".abf")
                {
                    files.Add(Path.GetFileNameWithoutExtension(fname));
                }
            }
            return files.ToArray();
        }

        /// <summary>
        /// Given an ABF path, return the ABFID of its parent (or "orphan" if none)
        /// </summary>
        private string GetParentForABF(string abfPath)
        {
            string[] abfIDs = GetFileIDs(true);
            string[] otherIDs = GetFileIDs(false);
            string parent = "orphan";
            foreach (string abfID in abfIDs)
            {
                foreach (string otherID in otherIDs)
                {
                    if (otherID.StartsWith(abfID))
                    {
                        parent = abfID;
                    }
                }
                if (abfID == Path.GetFileNameWithoutExtension(abfPath))
                {
                    return parent;
                }
            }
            return parent;
        }

        /// <summary>
        /// re-scan the ABF folder and its data folder
        /// </summary>
        private void ScanFolders()
        {
            folderContents = GetFolderContents(folderPath);
            dataFolderContents = GetFolderContents(dataFolderPath);

            // populate list of ABFs
            abfs = new List<ABFinfo>();
            foreach (string abfPath in folderContents)
            {
                if (Path.GetExtension(abfPath).ToLower() == ".abf")
                {
                    ABFinfo abf = new ABFinfo(abfPath);
                    abf.parent = GetParentForABF(abfPath);
                    abfs.Add(abf);
                }
            }

            // populate list of supportive images
            string[] imageExtensions = new string[] { ".png", ".jpg", ".jpeg", ".gif" };
            List<string> justImages = new List<string>();
            foreach (string imagePath in dataFolderContents)
            {
                if (imageExtensions.Contains(Path.GetExtension(imagePath).ToLower()))
                {
                    justImages.Add(imagePath);
                }
            }
            dataImages = justImages.ToArray();
        }

        /// <summary>
        /// return an ABFinfo object for the given ABF
        /// </summary>
        /// <returns></returns>
        public ABFinfo GetABFbyPath(string abfPath)
        {
            foreach (ABFinfo abf in abfs)
            {
                if (abf.path == abfPath)
                    return abf;
            }
            return null;
        }

        /// <summary>
        /// Prepare a DataTable for all ABFs in the folder
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable(bool parentsOnly=false, bool blankOrphans=true)
        {
            DataTable table = new DataTable();
            table.Columns.Add("abfID", typeof(string));
            table.Columns.Add("parent", typeof(string));
            table.Columns.Add("path", typeof(string));
            table.Columns.Add("protocol", typeof(string));
            table.Columns.Add("units", typeof(string));
            table.Columns.Add("annotations", typeof(string));
            table.Columns.Add("sweeps", typeof(int));
            table.Columns.Add("channels", typeof(int));
            table.Columns.Add("length (min)", typeof(double));
            table.Columns.Add("size (Mb)", typeof(double));
            table.Columns.Add("group", typeof(string));
            table.Columns.Add("tags", typeof(string));
            table.Columns.Add("color", typeof(string));

            foreach (ABFinfo abf in abfs)
            {
                if (parentsOnly && abf.abfID != abf.parent)
                    continue;
                DataRow row = table.NewRow();
                row.SetField(0, abf.abfID);
                row.SetField(1, abf.parent);
                row.SetField(2, abf.path);
                row.SetField(3, abf.protocol);
                row.SetField(4, abf.units);
                row.SetField(5, abf.annotations);
                row.SetField(6, abf.sweeps);
                row.SetField(7, abf.channels);
                row.SetField(8, abf.lengthMinutes);
                row.SetField(9, abf.sizeMB);
                row.SetField(10, abf.group);
                row.SetField(11, abf.comments);
                row.SetField(12, abf.color);

                if (blankOrphans == true && abf.parent == "orphan")
                {
                    row.SetField(1, "");
                }

                table.Rows.Add(row);
            }
            
            return table;
        }

    }
}
