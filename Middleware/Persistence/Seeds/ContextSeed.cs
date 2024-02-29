using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Seeds
{
    public static class ContextSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
	        CreateEntities(modelBuilder);
        }

        private static void CreateEntities(ModelBuilder modelBuilder)
        {
	        var entities = RequestsEntities.CategoryEntity();
	        modelBuilder.Entity<CategoryEntity>().HasData(entities);
		}

    }
}
