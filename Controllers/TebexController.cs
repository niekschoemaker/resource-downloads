using System;
using System.Collections.Generic;
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
    public class TebexController : ControllerBase
    {
        private readonly ResourcesContext _context;

        public TebexController(ResourcesContext context)
        {
            _context = context;
        }

        // GET: api/Tebex
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DownloadKey>>> GetDownloadKeys()
        {
            return await _context.DownloadKeys.ToListAsync();
        }

        // GET: api/Tebex/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DownloadKey>> GetDownloadKey(int id)
        {
            var downloadKey = await _context.DownloadKeys.FindAsync(id);

            if (downloadKey == null)
            {
                return NotFound();
            }

            return downloadKey;
        }

        // PUT: api/Tebex/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDownloadKey(int id, DownloadKey downloadKey)
        {
            if (id != downloadKey.Id)
            {
                return BadRequest();
            }

            _context.Entry(downloadKey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DownloadKeyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // POST: api/Tebex
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostDownloadKey(TebexPayment tebexPayment)
        {
            //_context.DownloadKeys.Add(downloadKey);
            //await _context.SaveChangesAsync();

            if (tebexPayment.Packages == null)
            {
                return Ok();
            }

            foreach (var package in tebexPayment.Packages)
            {
                var resource = await _context.Resources.FirstOrDefaultAsync(p => p.PackageId == package.PackageId);
                if (resource != null)
                {
                    var randomString = RandomString(24);
                    while (await _context.DownloadKeys.AnyAsync(p => p.Key == randomString))
                    {
                        randomString = RandomString(24);
                    }
                    _context.DownloadKeys.Add(new DownloadKey
                    {
                        ResourceId = resource.Id,
                        Key = randomString
                    });
                }
            }

            await _context.SaveChangesAsync();
            

            //return CreatedAtAction("GetDownloadKey", new { id = downloadKey.Id }, downloadKey);
            return Ok();
        }

        // DELETE: api/Tebex/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DownloadKey>> DeleteDownloadKey(int id)
        {
            var downloadKey = await _context.DownloadKeys.FindAsync(id);
            if (downloadKey == null)
            {
                return NotFound();
            }

            _context.DownloadKeys.Remove(downloadKey);
            await _context.SaveChangesAsync();

            return downloadKey;
        }

        private bool DownloadKeyExists(int id)
        {
            return _context.DownloadKeys.Any(e => e.Id == id);
        }
    }
}
