using System.Reflection;
using Infrastructure.Extension;
using OpenAI.Extensions;
using Persistence.Impl;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", true, true);
configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();

//AddDbContext
builder.Services.AddOrUpdateDbContext(configuration);

// AddOrUpdate Api Configuration
var assembly = Assembly.GetAssembly(typeof(Program));
builder.Services.AddOrUpdateApiConfiguration(configuration, assembly!);

builder.Services.AddOrUpdateAppSettings(configuration);

//AddOrUpdate MediatR
builder.Services.AddOrUpdateMediatR();

//AddOrUpdate AddOrUpdateFluentValidator
builder.Services.AddOrUpdateFluentValidator();

//AddOrUpdate AutoMapper
builder.Services.AddOrUpdateAutoMapper();

//Exception mapper to error codes and messages
builder.Services.AddExceptionMapper();

//AddOrUpdate Serilog
builder.Services.AddOrUpdateSerilog(configuration);

// AddScopedServices
builder.Services.AddScopedServices();

// AddTransientServices
builder.Services.AddTransientServices();

builder.Services.AddOpenAIService(settings => { settings.ApiKey = "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"; });

builder.Services.AddOrUpdateBlob(configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerOpenApi();

//AddVersion
builder.Services.AddVersion();

builder.Services.ConfigureCookiePolicyOptions();

builder.Logging.AddSerilog();

builder.Services.AddSpaStaticFiles(spaStaticFilesOptions =>
{
    spaStaticFilesOptions.RootPath = "ClientApp/dist";
});



var app = builder.Build();

await app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.ConfigureSwagger();
    app.UseHsts();

}

if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
}

app.UseRouting();

//Seed data
if (app.Environment.EnvironmentName == "Local")
{
    app.UseItToSeedSqlServer();
}

app.UseHttpsRedirection();
app.ConfigureCustomExceptionMiddleware();

app.MapControllers();

app.UseStaticFiles();

app.UseCookiePolicy();

app.ConfigureCors(configuration);

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";

    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    }
});

app.Run();



