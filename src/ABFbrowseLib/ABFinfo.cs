using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ABFbrowseLib
{
    public class ABFinfo
    {
        public string path;
        public string abfID;
        public string parent = "?";
        public string protocol = "?";
        public string units = "?";
        public string comments = "?";
        public int channels;
        public int sweeps;
        public double lengthMinutes = 0;
        public double sizeMB = 0;
        public string group = "?";
        public string annotations = "?";
        public string color = "gray";
        public string fileFormat = "?";
        public double sampleRate;

        public ABFinfo(string abfFilePath, bool readValues=true)
        {
            path = Path.GetFullPath(abfFilePath);
            abfID = Path.GetFileNameWithoutExtension(path);

            if (readValues)
                ReadValues();

        }

        private BinaryReader br;
        private void ReadValues()
        {
            // look up file size
            sizeMB = new System.IO.FileInfo(path).Length / 1e6;
            sizeMB = Math.Round(sizeMB, 2);

            // prepare the file reader and determine ABF version
            br = new BinaryReader(File.Open(path, FileMode.Open));
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            fileFormat = System.Text.Encoding.Default.GetString(br.ReadBytes(4));

            if (fileFormat == "ABF2")
            {
                ReadHeaderABF2();
            }
            else if (fileFormat == "ABF ")
            {
                fileFormat = "ABF1";
                ReadHeaderABF1();
            } else
            {
                fileFormat = "unknown";
            }
            br.Close();
        }

        private void ReadHeaderABF1()
        {
            // byte information is found on: https://github.com/swharden/pyABF

            // sweep count (lActualEpisodes) is an unsigned int at byte 16
            br.BaseStream.Seek(16, SeekOrigin.Begin);
            sweeps = (int)br.ReadUInt32();

            // nADCNumChannels is a short at byte 120
            br.BaseStream.Seek(120, SeekOrigin.Begin);
            channels = (int)br.ReadUInt16();

            // fADCSampleInterval is a float at byte 122
            br.BaseStream.Seek(122, SeekOrigin.Begin);
            double fADCSampleInterval = br.ReadSingle();

            // sample rate is the inverse of fADCSampleInterval(in microseconds)
            sampleRate = 1e6 / fADCSampleInterval;

            // lActualAcqLength is a signed integer at byte 10
            br.BaseStream.Seek(10, SeekOrigin.Begin);
            int lActualAcqLength = (int)br.ReadInt32();

            // use these values to determine the full abf length
            lengthMinutes = lActualAcqLength / sampleRate / channels / 60.0;
            lengthMinutes = Math.Round(lengthMinutes, 2);

            // pull units from first of 16 sADCUnits (8 characters at 602)
            br.BaseStream.Seek(602, SeekOrigin.Begin);
            units = System.Text.Encoding.Default.GetString(br.ReadBytes(8)).Trim();

            // protocol is not supported in ABF1 files
            protocol = "";

            // tags are not supported in ABF1 files
            annotations = "";
        }

        private void ReadHeaderABF2()
        {
            // byte information is found on: https://github.com/swharden/pyABF

            // sweep count (lActualEpisodes) is an unsigned int at byte 12
            br.BaseStream.Seek(12, SeekOrigin.Begin);
            sweeps = (int)br.ReadUInt32();

            // ADCsection item count is byte 92
            br.BaseStream.Seek(92, SeekOrigin.Begin);
            int adcSectionFirstByte = (int)br.ReadUInt32() * 512;
            int adcSectionItemSize = (int)br.ReadUInt32();
            int adcSectionItemCount = (int)br.ReadUInt32();
            channels = adcSectionItemCount;

            // determine where the protocolSection is (noted as an int at byte 76)
            br.BaseStream.Seek(76, SeekOrigin.Begin);
            int protocolSectionFirstByte = (int)br.ReadUInt32() * 512;

            // fADCSequenceInterval is a float at protocolSection +2
            br.BaseStream.Seek(protocolSectionFirstByte + 2, SeekOrigin.Begin);
            double fADCSequenceInterval = br.ReadSingle();

            // sample rate is the inverse of fADCSequenceInterval (in microseconds)
            sampleRate = 1e6 / fADCSequenceInterval;

            // determine where the data section is (noted as an int at byte 236)
            br.BaseStream.Seek(236, SeekOrigin.Begin);
            int dataSectionFirstByte = (int)br.ReadUInt32() * 512;
            int dataSectionItemSize = (int)br.ReadUInt32();
            int dataSectionItemCount = (int)br.ReadUInt32();

            // use these values to determine the full abf length
            lengthMinutes = dataSectionItemCount / sampleRate / channels / 60.0;
            lengthMinutes = Math.Round(lengthMinutes, 2);

            // determine StringsSection byte location (found at byte 220)
            br.BaseStream.Seek(220, SeekOrigin.Begin);
            int stringsSectionFirstByte = (int)br.ReadUInt32() * 512;
            int stringsSectionItemSize = (int)br.ReadUInt32();
            int stringsSectionItemCount = (int)br.ReadUInt32();

            // read the first string of the strings section
            br.BaseStream.Seek(stringsSectionFirstByte, SeekOrigin.Begin);
            string stringsRaw = System.Text.Encoding.Default.GetString(br.ReadBytes(stringsSectionItemSize));
            stringsRaw = stringsRaw.Substring(stringsRaw.LastIndexOf("\x00\x00"));
            stringsRaw = stringsRaw.Replace("\xb5", "\x75"); // make mu u
            string[] strings = stringsRaw.Split('\x00');
            strings = strings.Skip(1).ToArray();

            // the ADC units is the adc section's lADCUnitsIndex'th string
            br.BaseStream.Seek(adcSectionFirstByte + 78, SeekOrigin.Begin);
            int lADCUnitsIndex = (int)br.ReadInt32();
            units = strings[lADCUnitsIndex];

            // the protocol is the uProtocolPathIndex'th string
            br.BaseStream.Seek(72, SeekOrigin.Begin);
            int uProtocolPathIndex = (int)br.ReadUInt32();
            protocol = strings[uProtocolPathIndex];

            // tags come from the tagSection (described at byte 252)
            br.BaseStream.Seek(252, SeekOrigin.Begin);
            int tagSectionFirstByte = (int)br.ReadUInt32() * 512;
            int tagSectionItemSize = (int)br.ReadUInt32();
            int tagSectionItemCount = (int)br.ReadUInt32();

            // tags are 56 character strings
            string[] eachTag = new string[tagSectionItemCount];
            for (int i=0; i< tagSectionItemCount; i++)
            {
                int tagFirstByte = tagSectionFirstByte + i * tagSectionItemSize;
                br.BaseStream.Seek(tagFirstByte, SeekOrigin.Begin);
                int tagTime = (int)br.ReadUInt32();
                string tagComment = System.Text.Encoding.Default.GetString(br.ReadBytes(56));
                eachTag[i] = tagComment.Trim();
            }
            annotations = string.Join(", ", eachTag);
        }
    }
}
