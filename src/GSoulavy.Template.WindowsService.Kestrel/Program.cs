using GSoulavy.Template.WindowsService.Kestrel.Configurations;
using GSoulavy.Template.WindowsService.Kestrel.Services;

using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Hosting.WindowsServices;

using Serilog;

var builder = WebApplication.CreateBuilder(
    new WebApplicationOptions
    {
        Args = args,
        ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
    }
);

var config = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddJsonFile("appSettings.json", false, true)
    .AddJsonFile($"appSettings.{builder.Environment.EnvironmentName}.json", true)
    .Build();

builder.Host
    .UseWindowsService()
    .UseSerilog(
        (hostingContext, _, loggerConfiguration) => loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
    );

builder.Services
    .AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .Configure<JsonOptions>(
        options => { options.SerializerOptions.IncludeFields = true; }
    )
    .Configure<JsonOptions>(
        options => { options.SerializerOptions.IncludeFields = true; }
    )
    .AddEndpointsApiExplorer()
    .Configure<HostedSettings>(config.GetSection(nameof(HostedSettings)))
    .AddSwaggerGen()
    .AddHostedService<HostedService>()
    .AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");

app.MapControllers();

app.Urls
    .Add($"http://localhost:{config.GetValue<string>("Kestrel:PortNumber")}");

await app
    .RunAsync();
