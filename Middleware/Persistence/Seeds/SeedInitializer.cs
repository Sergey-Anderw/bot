using Persistence.Impl;

namespace Persistence.Seeds
{
	public class SeedInitializer
	{
		public static void Initialize(AppDbContext dbContext)
		{
			ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
			dbContext.Database.EnsureCreated();
			/*if (dbContext.Categories.Any()) 
				return;

			var entities = RequestsEntities.CategoryEntity();
			dbContext.Categories.AddRange(entities);*/
			
			dbContext.SaveChanges();
		}
	}
}
