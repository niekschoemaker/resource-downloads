using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ResourceDownloads.Models;

namespace ResourceDownloads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TebexController : ControllerBase
    {
        private readonly ResourcesContext _context;
        private readonly ILogger _logger;

        public TebexController(ResourcesContext context, ILogger<TebexController> logger)
        {
            _logger = logger;
            _context = context;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpGet]
        public async Task<IActionResult> GetTebexPayment(TebexPayment tebexPayment)
        {
            _logger.LogError(JsonConvert.SerializeObject(tebexPayment));

            return Ok();
        }

        // POST: api/Tebex
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostDownloadKey(TebexPayment tebexPayment)
        {
            //_context.DownloadKeys.Add(downloadKey);
            //await _context.SaveChangesAsync();

            if (Startup.AppSettings.TebexSecret != null)
            {
                if (!Request.Headers.ContainsKey("X-BC-Sig"))
                {
                    return NotFound();
                }
                using (var sha256 = SHA256.Create())
                {
                    var authorizationHeaders = Request.Headers["X-BC-Sig"].ToString().ToUpper();

                    var hash = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(Startup.AppSettings.TebexSecret + tebexPayment.Payment.TxnId + tebexPayment.Payment.Status + tebexPayment.Customer.Email))).Replace("-", "");
                    if (hash != authorizationHeaders)
                    {
                        return NotFound();
                    }
                }
            }
            
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

            _logger.LogError(JsonConvert.SerializeObject(tebexPayment));
            //return CreatedAtAction("GetDownloadKey", new { id = downloadKey.Id }, downloadKey);
            return Ok();
        }
    }
}
