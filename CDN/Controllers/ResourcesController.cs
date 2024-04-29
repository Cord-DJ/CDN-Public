using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Cord.CDN.Controllers;

[ApiController]
public class ResourcesController : ControllerBase {
    public ResourcesController() {
        
    }

    [HttpGet("embed/avatars/{id}.webp")]
    public async Task<IActionResult> Embed(int id, [FromQuery] int? size) {
        if (id < 0 || id > 4) {
            return BadRequest("out of range");
        }

        if (size != null && (size.Value > 4096 || size.Value < 16)) {
            return BadRequest("wrong size");
        }

        try {
            var bytes = await System.IO.File.ReadAllBytesAsync($"embed/avatars/{id}.webp");
            bytes = ImageService.GetImageAsBytes(size, bytes);

            using var sha = System.Security.Cryptography.SHA1.Create();
            var crc = sha.ComputeHash(bytes);
            var checksum = $"\"{WebEncoders.Base64UrlEncode(crc)}\"";
            
            var file = File(bytes, "image/webp");
            file.EntityTag = new Microsoft.Net.Http.Headers.EntityTagHeaderValue(checksum);

            return file;
        } catch (FileNotFoundException) {
            return NotFound("not found");
        }
    }

    [HttpGet("{type}/{hash}.webp")]
    public async Task<IActionResult> GetResource(string type, string hash, [FromQuery] int? size) {
        if (!ImageService.Resources.Contains(type)) {
            return BadRequest("unknown resource type");
        }

        if (size != null && (size.Value > 4096 || size.Value < 16)) {
            return BadRequest("wrong size");
        }

        try {
            var bytes = await System.IO.File.ReadAllBytesAsync($"../resources/{type}/{hash}.webp");
            bytes = ImageService.GetImageAsBytes(size, bytes);

            using var sha = System.Security.Cryptography.SHA1.Create();
            var crc = sha.ComputeHash(bytes);
            var checksum = $"\"{WebEncoders.Base64UrlEncode(crc)}\"";
            
            var file = File(bytes, "image/webp");
            file.EntityTag = new Microsoft.Net.Http.Headers.EntityTagHeaderValue(checksum);

            return file;
        } catch (FileNotFoundException) {
            return NotFound("not found");
        }
    }
}
