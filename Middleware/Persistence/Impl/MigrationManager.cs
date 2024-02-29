using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Impl
{
    public static class MigrationManager
    {
        public static async Task MigrateDatabase(this WebApplication host)
        {
            using var serviceScope = host.Services.CreateScope();
            await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync();
            
        }
    }
}
