using Microsoft.AspNetCore.Mvc;
using RoadStat.Core.Models;
using RoadStat.Core.Services;

namespace RoadStat.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected readonly IEntityService<CarSpeedEntry> _entityService;

        public BaseApiController(IEntityService<CarSpeedEntry> entityService)
        {
            _entityService = entityService;
        }
    }
}
