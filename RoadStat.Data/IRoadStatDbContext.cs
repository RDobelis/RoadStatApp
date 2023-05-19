using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RoadStat.Core.Models;

namespace RoadStat.Data
{
    public interface IRoadStatDbContext 
    {
        public DbSet<CarSpeedEntry> CarSpeedEntries { get; set; }
        DbSet<T> Set<T>() where T : class;
        EntityEntry<T> Entry<T>(T entity) where T : class;
        Task<int> SaveChangesAsync();
    }
}
