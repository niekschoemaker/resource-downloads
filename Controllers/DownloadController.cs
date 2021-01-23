using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceDownloads.Models;

namespace ResourceDownloads.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly ResourcesContext context;
        public DownloadController(ResourcesContext _context)
        {
            context = _context;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetFile(string key, [FromQuery(Name = "version")] string version)
        {
            var downloadInfo = context.DownloadKeys.Include(p => p.Resource).FirstOrDefault(p => p.Key == key);
            if (downloadInfo == null)
            {
                return NotFound();
            }

            var path = downloadInfo.Resource.Path;

            if (version != null)
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
    }
}
