using Microsoft.AspNetCore.Mvc;

namespace Cord.CDN.Controllers;

[ApiController]
public class UploadsController : ControllerBase {
    readonly IConfiguration configuration;

    public UploadsController(IConfiguration configuration) {
        this.configuration = configuration;
    }

    [HttpPost("upload/{type}")]
    public async Task<IActionResult> Upload(string type, [FromBody] UploadModel model) {
        if (HttpContext.Request.Headers["Authorization"] != configuration.GetSection("authToken").Value) {
            throw new UnauthorizedException();
        }
        
        if (!ImageService.Resources.Contains(type)) {
            return BadRequest("unknown resource type");
        }

        var hash = await ResourceManager.SaveResource(model.Data, type);
        return Ok(hash);
    }
}

public record UploadModel(ID UserID, string Data);
