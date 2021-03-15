namespace GSoulavy.Template.WindowsService.Kestrel
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Hosting;

    using Serilog;

    internal class Program
    {
        private static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseSerilog(
                    (hostingContext, _, loggerConfiguration) => loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                );

        /*   .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>();})
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
           .ConfigureServices(
               (hostContext, services) =>
               {
                   services.Configure<HostedSettings>(
                       hostContext.Configuration.GetSection(nameof(HostedSettings))
                   );

               }
           );
       */
    }
}
