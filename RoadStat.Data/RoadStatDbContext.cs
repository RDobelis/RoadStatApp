using Microsoft.EntityFrameworkCore;
using RoadStat.Core.Models;

namespace RoadStat.Data
{
    public class RoadStatDbContext : DbContext, IRoadStatDbContext
    {
        public RoadStatDbContext(DbContextOptions<RoadStatDbContext> options) : base(options) { }

        public DbSet<CarSpeedEntry> CarSpeedEntries { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
