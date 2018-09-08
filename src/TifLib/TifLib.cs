using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TifLib
{
    public class TifFile
    {
        public string filePath;
        public long fileSize;
        public bool validTif;

        public Logger log;

        private bool littleEndian;

        public TifFile(string filePath)
        {
            filePath = Path.GetFullPath(filePath);
            this.filePath = filePath;
            log = new Logger("TifLib");
            log.Info($"Loading abf file: {filePath}");

            if (!File.Exists(filePath))
                throw new Exception($"file does not exist: {filePath}");

            fileSize = new System.IO.FileInfo(filePath).Length;
            validTif = true;
            FileOpen();
            ReadHeader();
            FileClose();
        }

        BinaryReader br;
        public void FileOpen()
        {
            log.Debug("opening file");
            br = new BinaryReader(File.Open(filePath, FileMode.Open));
            br.BaseStream.Seek(0, SeekOrigin.Begin);
        }

        public void FileClose()
        {
            log.Debug("file closed");
            br.Close();
        }

        private byte[] FileReadBytes(int charCount, int bytePosition = -1)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return br.ReadBytes(charCount);
        }

        private int FileReadUInt32(int bytePosition = -1)
        {
            byte[] bytes = FileReadBytes(4, bytePosition);
            if (!littleEndian)
                Array.Reverse(bytes);
            return (int) BitConverter.ToUInt32(bytes, 0);
        }

        private string BytesToString(byte[] bytes)
        {
            string s = System.Text.Encoding.Default.GetString(bytes);
            return s;
        }

        private string BytesToHexstring(byte[] bytes)
        {
            string hexString = "";
            foreach (byte b in bytes)
                hexString += String.Format("{0:X2}", Convert.ToInt32(b));
            return hexString;
        }

        private string BytesToFormattedString(byte[] bytes)
        {
            string[] strBytes = new string[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                strBytes[i] = string.Format("{0}", bytes[i]);
            }
            string s = string.Join(", ", strBytes);
            return $"[{s}]";
        }

        public void ReadHeader()
        {
            // READ THE IDENTIFIER
            // the identifier says whether data is little-endian or big-endian.
            // it is always the first two bytes of the file
            // "II" (4949) for little-endian
            // "MM" (4D4D) for big-endian
            // anything else indicates this is not a valid TIF file
            string identifier = BytesToHexstring(FileReadBytes(2, 0));
            string version = BytesToHexstring(FileReadBytes(2));
            string firstFour = identifier + version;

            log.Debug($"Identifier: {identifier}");
            log.Debug($"Version: {version}");

            if (firstFour == "49492A00")
            {
                littleEndian = true;
            }
            else if (firstFour == "4D4D002A")
            {
                littleEndian = false;
            }
            else
            {
                validTif = false;
                log.Critical($"Invalid TIF identifier and/or version");
                return;
            }

            // READ THE OFFSET
            // IFDOffset is a 32 - bit value that is the offset position of the first 
            // Image File Directory in the TIFF file. This value may be passed as a 
            // parameter to a file seek function to find the start of the image file information.
            // If the Image File Directory occurs immediately after the header, the value of the 
            // IFDOffset field is 08h.
            int IFDOffset = FileReadUInt32();
            log.Debug($"IFDOffset: {IFDOffset}");
            if (IFDOffset > fileSize)
            {
                validTif = false;
                log.Critical($"invalid IFDOffset: {IFDOffset}");
                return;
            }

        }

        public string Info()
        {
            string msg = $"File: {System.IO.Path.GetFileName(filePath)}\n";
            msg += $"Full Path: {filePath}\n";
            msg += $"Valid TIF: {validTif}\n";
            if (!validTif) return msg;
            msg += $"Little Endian: {littleEndian}\n";
            return msg;
        }
    }
}
