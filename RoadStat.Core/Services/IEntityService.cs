using Microsoft.AspNetCore.Http;
using RoadStat.Core.Models;

namespace RoadStat.Core.Services
{
    public interface IEntityService<T> where T : Entity
    {
        Task UploadFile(IFormFile file);

        Task DeleteAll();

        IQueryable<T> GetFilteredData(FilterRequestModel request);

        IEnumerable<AverageSpeedResult> GetAverageSpeed(DateTime date);
    }
}
