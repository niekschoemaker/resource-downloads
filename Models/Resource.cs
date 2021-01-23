using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceDownloads.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string ResourceName { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public int PackageId { get; set; }
        public string LatestVersion { get; set; }

        public virtual List<Version> Versions { get; set; }
        public virtual List<DownloadKey> DownloadKeys { get; set; }

        public bool ShouldSerializeLazyLoader() { return false; }
    }
}
