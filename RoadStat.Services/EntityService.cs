using Microsoft.AspNetCore.Http;
using RoadStat.Core.Models;
using RoadStat.Core.Services;
using RoadStat.Data;

namespace RoadStat.Services
{
    public class EntityService<T> : IEntityService<T> where T : Entity
    {
        protected readonly IRoadStatDbContext _context;
        private readonly IFileParser<T> _fileParser;

        public EntityService(IRoadStatDbContext context, IFileParser<T> fileParser)
        {
            _context = context;
            _fileParser = fileParser;
        }
        
        public async Task UploadFile(IFormFile file)
        {
            var uniqueEntries = await _fileParser.Parse(file);

            _context.Set<T>().AddRange(uniqueEntries);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAll()
        {
            _context.Set<T>().RemoveRange(_context.Set<T>());
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetFilteredData(FilterRequestModel request)
        {
            var query = _context.Set<T>().AsQueryable();

            if (request.MinSpeed.HasValue)
                query = query.Where(entry => entry.Speed >= request.MinSpeed.Value);

            if (request.DateFrom.HasValue)
            {
                var dateFrom = request.DateFrom.Value.ToLocalTime();
                query = query.Where(entry => entry.Date >= dateFrom);
            }

            if (request.DateTo.HasValue)
            {
                var dateTo = request.DateTo.Value.ToLocalTime().AddDays(1).AddTicks(-1);
                query = query.Where(entry => entry.Date <= dateTo);
            }

            if (request.Page.HasValue)
                query = query.Skip((request.Page.Value - 1) * FilterRequestModel.FixedPageSize).Take(FilterRequestModel.FixedPageSize);

            return query;
        }

        public IEnumerable<AverageSpeedResult> GetAverageSpeed(DateTime date)
        {
            var startDate = date.Date;
            var endDate = date.Date.AddDays(1);

            var query = _context.Set<T>()
                .Where(entry => entry.Date >= startDate && entry.Date < endDate)
                .GroupBy(entry => entry.Date.Hour)
                .Select(group => new AverageSpeedResult { Hour = group.Key, AverageSpeed = group.Average(entry => entry.Speed) })
                .ToList();

            return query;
        }

    }
}
