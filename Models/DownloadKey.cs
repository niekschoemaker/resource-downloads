using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceDownloads.Models
{
    public class DownloadKey
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public int ResourceId { get; set; }
        public virtual Resource Resource { get; set; }

        public bool ShouldSerializeLazyLoader() { return false; }
    }
}
