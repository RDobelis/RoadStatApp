using Microsoft.AspNetCore.Mvc;
using RoadStat.Core.Models;
using RoadStat.Core.Services;

namespace RoadStat.Controllers
{
    [Route("api/FileUpload")]
    [ApiController]
    public class FileUploadController : BaseApiController
    {
        public FileUploadController(IEntityService<CarSpeedEntry> entityService) : base(entityService)
        {
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not found.");

            await _entityService.UploadFile(file);

            return Ok("File imported successfully.");
        }
    }
}
