using System.Reflection;
using Application.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.Impl;
using Persistence.Interfaces;
using Shared.Configuration;

namespace Infrastructure.Extension
{
    public static class DependencyInjection
    {
        /// <summary>
        /// AddScopedServices
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddScopedServices(this IServiceCollection serviceCollection)
        {
            //todo mock, ctor, unit
            serviceCollection.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>()!);
        }

        /// <summary>
        /// AddTransientServices
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddTransientServices(this IServiceCollection serviceCollection)
        {
	        //serviceCollection.AddTransient<IIdentityApi, IdentityApi>();
            // Inject ClaimsPrincipal
            serviceCollection.AddTransient(provider => provider.GetService<IHttpContextAccessor>()!.HttpContext?.User);

		}

		/// <summary>
		/// AddOrUpdate Api Configuration
		/// </summary>
		/// <param name="serviceCollection"></param>
		/// <param name="configuration"></param>
		/// <param name="assembly"></param>
		public static void AddOrUpdateApiConfiguration(this IServiceCollection serviceCollection,
	        ConfigurationManager configuration, Assembly assembly)
        {
	        var currentDir = Path.GetDirectoryName(assembly?.Location);
	        var appDataFullDirName = Path.Combine(currentDir!, "App_Data");
	        serviceCollection.AddSingleton(
		        provider =>
		        {
			        var apiSettings = new GatewayOptions();
			        configuration.GetSection("GatewayOptions").Bind(apiSettings);

			        apiSettings.AppDataPath = appDataFullDirName;

			        SharedAppSettings.EncryptState = apiSettings.EncryptState;
			        SharedAppSettings.CommunicationStateEncryptionKey = apiSettings.CommunicationStateEncryptionKey;

			        return apiSettings;
		        });

		}

		/// <summary>
		/// AppSettings
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static AppSettings AddOrUpdateAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
	        services.Configure<AppSettings>(configuration);
	        services.AddSingleton(
		        cfg => cfg.GetService<IOptions<AppSettings>>()!.Value);

	        var serviceProvider = services.BuildServiceProvider();

	        return serviceProvider.GetService<AppSettings>()!;
        }



		/// <summary>
		/// ConfigureCookiePolicyOptions
		/// </summary>
		/// <param name="serviceCollection"></param>
		public static void ConfigureCookiePolicyOptions(this IServiceCollection serviceCollection)
        {
            serviceCollection.Configure<CookiePolicyOptions>(
                options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });
        }
    }
}
