using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TifLib
{
    public class TifFile
    {
        public Logger log;
        public TifFile(string filePath)
        {
            log = new Logger("TifLib");
            log.Info($"Loading abf file: {filePath}");
        }

        public string Info()
        {
            return log.logText;
        }
    }
}
