using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Persistence.Impl
{
    public class AppDbContext : DbContext, IAppDbContext
    {
	    public AppDbContext()
	    {

	    }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
	        
			modelBuilder.ApplyConfigurationsFromAssembly(assembly: typeof(AppDbContext).Assembly);            
            //modelBuilder.Seed();
		}

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("DataSource=app.db");
            }

        }

		public virtual DbSet<CategoryEntity> Categories { get; set; }


		public new virtual DbSet<TEntity> Set<TEntity>() where TEntity : class   // This is virtual because Moq needs to override the behaviour 
        {
            return base.Set<TEntity>();
        }
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

	}
}
