using HealMonitoring.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Load services

builder.Services.AddEntityFrameworkMySql()
    .AddDbContext<HealMonitoringContext>(options => 
        options.UseMySql(app.Configuration.GetConnectionString("HealMonitoring"), new MySqlServerVersion(new Version(8, 0, 30))));

try
{
	using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>()
		.CreateScope())
	{
		serviceScope.ServiceProvider.GetService<HealMonitoringContext>()
				.Database.Migrate();
	}
}
catch { }


app.Run();
