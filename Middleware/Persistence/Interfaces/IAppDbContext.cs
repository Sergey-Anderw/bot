using Microsoft.EntityFrameworkCore;

namespace Persistence.Interfaces
{
    public interface IAppDbContext
    {        
        //DbSet<SiteEntity> Sites { get; set; }

		DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}
