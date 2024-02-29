using Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Impl;
using Persistence.Seeds;

namespace Infrastructure.Extension
{
    public static class ConfigureContainer
    {
        public static void ConfigureCors(this IApplicationBuilder app, ConfigurationManager configuration)
        {
#if !DEBUG

            var origins = configuration.GetSection("Origins").Get<string[]>();
            app.UseCors(opt => {
                opt.WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
#endif
        }

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
		}


        /*public static void ConfigureIgnoreRouteMiddleware(this IApplicationBuilder app)
        {
	        app.UseMiddleware<IgnoreRouteMiddleware>();
        }*/


        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI();
        }

        public static void UseItToSeedSqlServer(this IApplicationBuilder app)
        {
	        ArgumentNullException.ThrowIfNull(app, nameof(app));

	        using var scope = app.ApplicationServices.CreateScope();
	        var services = scope.ServiceProvider;
	        var context = services.GetRequiredService<AppDbContext>();
	        SeedInitializer.Initialize(context);
		}


	}
}
