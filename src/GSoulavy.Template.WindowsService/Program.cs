namespace GSoulavy.Template.WindowsService
{
    using System;

    using Configurations;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using Serilog;

    using Services;

    internal class Program
    {
        private static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureAppConfiguration(
                    (ctx, builder) =>
                    {
                        if (!ctx.HostingEnvironment.IsDevelopment())
                            builder.SetBasePath(Environment.GetEnvironmentVariable("ServiceBasePath"));

                        builder
                            .AddJsonFile("appsettings.json", false, true)
                            .AddJsonFile("appsettings.log.json", false, true)
                            .AddJsonFile(
                                $"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json",
                                true,
                                true
                            )
                            .AddEnvironmentVariables();
                    }
                )
                .UseSerilog(
                    (hostingContext, services, loggerConfiguration) => loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                )
                .ConfigureServices(
                    (hostContext, services) =>
                    {
                        services.Configure<HostedSettings>(
                            hostContext.Configuration.GetSection(nameof(HostedSettings))
                        );

                        services.AddHostedService<HostedService>();
                    }
                );
    }
}
