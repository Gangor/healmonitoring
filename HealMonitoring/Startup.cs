namespace HealMonitoring
{
    using HealMonitoring.DB;
    using HealMonitoring.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Hosting;
    using System;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));
            var connectionString = Configuration.GetConnectionString("HealMonitoring");

            services.AddDbContext<HealMonitoringContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, serverVersion)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

            services.AddControllers();

            services.AddHealMonitoringStore<HealMonitoringContext>();

            // Hosted Services

            // Github metrics

            //services.AddSingleton<GithubService>();
            //services.AddHostedService(provider => provider.GetService<GithubService>());

            // Saleforce metrics

            services.AddSingleton<SaleForceService>();
            services.AddHostedService(provider => provider.GetService<SaleForceService>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

        }
    }
}
