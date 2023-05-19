using Microsoft.AspNetCore.Mvc;
using RoadStat.Core.Models;
using RoadStat.Core.Services;

namespace RoadStat.Controllers
{
    [Route("api/cleanup")]
    [ApiController]
    public class CleanUpController : BaseApiController
    {
        public CleanUpController(IEntityService<CarSpeedEntry> entityService) : base(entityService)
        {
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await _entityService.DeleteAll();
            return Ok("All data deleted.");
        }
    }
}
