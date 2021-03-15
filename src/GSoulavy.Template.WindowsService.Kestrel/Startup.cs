namespace GSoulavy.Template.WindowsService.Kestrel
{
    using System;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Text.Json;

    using Configurations;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    using Services;

    public class Startup
    {
        public Startup(IConfiguration configuration) { Configuration = configuration; }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddControllers()
                .AddJsonOptions(
                    options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                        options.JsonSerializerOptions.IgnoreNullValues = true;
                    }
                );

            services.Configure<KestrelServerOptions>(
                options => { options.Listen(IPAddress.Loopback, Configuration.GetValue<int>("Kestrel:PortNumber")); }
            );

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        "v1",
                        new OpenApiInfo
                        {
                            Title = "GSoulavy.Template.WindowsService.Kestrel",
                            Version = "v1",
                            Description = "This is a Windows Service Template on dotnet 5"
                        }
                    );

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                }
            );

            services.Configure<HostedSettings>(Configuration.GetSection(nameof(HostedSettings)));

            services.AddHostedService<HostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Endpoint v1"));
            }

            app.UseRouting();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapHealthChecks("/health");
                    endpoints.MapGet(
                        "/",
                        async context =>
                        {
                            await context.Response.WriteAsync("GSoulavy.Template.WindowsService.Kestrel");
                        }
                    );

                    endpoints.MapControllers();
                }
            );
        }
    }
}
