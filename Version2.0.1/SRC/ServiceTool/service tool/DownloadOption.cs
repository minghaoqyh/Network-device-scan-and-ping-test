using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service_tool
{
    public class DownloadOption
    {
        public PCBAType PcbaType { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int DownloadIndex { get; set; }
        public string DownloadResult { get; set; }
        public bool DownloadSuccess { get; set; }
        public string AppVersion { get; set; }
        public string CoreVersion { get; set; }
    }
}
