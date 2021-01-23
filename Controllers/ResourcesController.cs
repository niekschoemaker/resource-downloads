using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceDownloads.Models;

namespace ResourceDownloads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly ResourcesContext _context;

        public ResourcesController(ResourcesContext context)
        {
            _context = context;
        }

        // GET: api/Resources/esx_cruisecontrol/latest?key=luvPIbtlLJu2fZ5aJ3QceFxC
        [HttpGet("{name}/{version?}")]
        public async Task<IActionResult> GetLatestResource(string name, [FromQuery] string key, string version = "latest")
        {
            var downloadInfo = _context.DownloadKeys.Include(p => p.Resource).FirstOrDefault(p => p.Key == key && p.Resource.ResourceName == name);
            if (downloadInfo == null)
            {
                return NotFound();
            }
            var path = downloadInfo.Resource.Path;

            if (version != null && version != "latest")
            {
                var versionPath = downloadInfo.Resource.Versions.FirstOrDefault(p => p.VersionIdentifier == version);
                if (versionPath == null)
                {
                    return NotFound(new
                    {
                        Error = $"Version {version} doesn't exist!",
                        PossibleValues = downloadInfo.Resource.Versions.Select(p => p.VersionIdentifier).ToList()
                    });
                }
            }

            if (!System.IO.File.Exists(path))
            {
                return NotFound("File not found!");
            }

            Stream stream = System.IO.File.OpenRead(downloadInfo.Resource.Path);
            return File(stream, "application/octet-stream", downloadInfo.Resource.FileName ?? Path.GetFileName(downloadInfo.Resource.Path));
        }

        private bool ResourceExists(int id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }
    }
}
