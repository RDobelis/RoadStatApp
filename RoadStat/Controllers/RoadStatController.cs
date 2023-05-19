using Microsoft.AspNetCore.Mvc;
using RoadStat.Core.Models;
using RoadStat.Core.Services;

namespace RoadStat.Controllers
{
    [Route("api/RoadStat")]
    [ApiController]
    public class RoadStatController : BaseApiController
    {
        public RoadStatController(IEntityService<CarSpeedEntry> entityService) : base(entityService)
        {
        }

        [HttpGet("filtered")]
        public IActionResult GetFilteredData([FromQuery] FilterRequestModel request)
        {
            var result = _entityService.GetFilteredData(request).ToList();

            return Ok(result);
        }

        [HttpGet("average-speed")]
        public IActionResult GetAverageSpeed([FromQuery] DateTime date)
        {
            var query = _entityService.GetAverageSpeed(date);

            return Ok(query);
        }
    }
}
