using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceDownloads.Models
{
    public class Version
    {
        public int VersionId { get; set; }
        public string VersionIdentifier { get; set; }
        public string Path { get; set; }
        public virtual Resource Resource { get; set; }
    }
}
