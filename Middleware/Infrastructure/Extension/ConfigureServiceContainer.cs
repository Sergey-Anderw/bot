using System.Reflection;
using Application.Mappers;
using Asp.Versioning;
using AutoMapper;
using Azure.Storage.Blobs;
using FluentValidation;
using Master.Infrastructure.Pipeline;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Impl;
using Persistence.Interfaces;
using Serilog;
using Shared.AutoMapper;
using Shared.Interfaces.Mapping;
using ValidationException = Shared.Exceptions.ValidationException;

namespace Infrastructure.Extension
{
    public static class ConfigureServiceContainer
    {
        #region AddDbContext

        public static void AddOrUpdateDbContext(this IServiceCollection serviceCollection,
            ConfigurationManager configuration)
        {
	        serviceCollection.AddDbContext<AppDbContext>(options =>
	        {
		        options.UseSqlServer(configuration.GetConnectionString("DbConnection"),
			        b => b.MigrationsAssembly(typeof(IAppDbContext).Assembly.FullName));
		        options.EnableSensitiveDataLogging();
			});

        }

        #endregion



        #region AddOrUpdate MediatR

        public static void AddOrUpdateMediatR(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(CustomMapperProfile).GetTypeInfo().Assembly);
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        }

        #endregion

        #region AddOrUpdate FluentValidator

        public static void AddOrUpdateFluentValidator(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddValidatorsFromAssemblyContaining<ValidationException>();

        }
        #endregion

        #region Blob

        public static void AddOrUpdateBlob(this IServiceCollection serviceCollection,
            ConfigurationManager configuration)
        {
            serviceCollection.AddScoped(x => new BlobServiceClient(configuration.GetConnectionString("BlobConnection")));

        }

        #endregion


        #region AddOrUpdate AutoMapper

        public static void AddOrUpdateAutoMapper(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(
                _ =>
                {
                    var autoMapperProfile = AutoMapperProfile.Initialize(
                        typeof(CustomMapperProfile).Assembly);
                    var customMapperProfile = new CustomMapperProfile();
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(autoMapperProfile);
                        cfg.AddProfile(customMapperProfile);
                    });

                    return config.CreateMapper();
                });
        }

        #endregion

        #region Add Exception mapper

        public static void AddExceptionMapper(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IExceptionMapper, ExceptionMapper>();
        }

        #endregion

        #region AddOrUpdate Serilog

        public static void AddOrUpdateSerilog(this IServiceCollection serviceCollection, ConfigurationManager configuration)
        {
            serviceCollection.AddSingleton<ILogger>(
                _ =>
                {
                    var loggerConfiguration = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration);

                    var logger = loggerConfiguration.CreateLogger();

                    return logger;
                });
        }

        #endregion

        #region AddVersion

        public static void AddVersion(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        #endregion

        #region AddSwaggerOpenAPI

        public static void AddSwaggerOpenApi(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(setupAction =>
            {

                /*setupAction.SwaggerDoc(
                    "OpenAPISpecification",
                    new OpenApiInfo
                    {
                        Title = "WebAPI",
                        Version = "1",
                    });

                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = $"Input your Bearer token in this format - Bearer token to access this API",
                });
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        }, new List<string>()
                    },
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                setupAction.IncludeXmlComments(xmlCommentsFullPath);*/

            });

        }

        #endregion


        #region ConfigureHostOptions

        public static void ConfigureHostoptions(this IServiceCollection serviceCollection)
        {
            serviceCollection.Configure<HostOptions>(opts =>
            {
                opts.ShutdownTimeout = TimeSpan.FromSeconds(30);
                opts.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.StopHost;
            });
        }
        #endregion ConfigureHostOptions

    }
}
