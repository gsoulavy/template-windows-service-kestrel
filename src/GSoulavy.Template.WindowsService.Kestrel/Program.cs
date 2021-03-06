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
    }
}
