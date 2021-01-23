using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceDownloads.Models
{
    public class ResourcesContext : DbContext
    {
        public ResourcesContext(DbContextOptions<ResourcesContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var file = new Resource { Id = 1, Path = "/tmp/test.txt" };
            modelBuilder.Entity<Resource>().HasData(file);
            modelBuilder.Entity<DownloadKey>()
                .HasIndex(p => p.Key);
            modelBuilder.Entity<DownloadKey>()
                .HasData(new DownloadKey { Key = "test", Id = 1, ResourceId = 1 });
        }

        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<DownloadKey> DownloadKeys { get; set; }
        public virtual DbSet<Version> Versions { get; set; }
    }
}
