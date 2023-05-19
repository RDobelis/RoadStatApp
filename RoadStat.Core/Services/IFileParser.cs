using Microsoft.AspNetCore.Http;

namespace RoadStat.Core.Services
{
    public interface IFileParser<T>
    {
        Task<HashSet<T>> Parse(IFormFile file);
    }
}
