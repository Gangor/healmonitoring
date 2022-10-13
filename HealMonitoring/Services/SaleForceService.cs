using Saleforce;
using HealMonitoring.DB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HealMonitoring.Services
{
    public class SaleForceService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SaleForceService> _logger;

        private Timer _timer;

        public SaleForceService(
            IServiceScopeFactory scopeFactory,
            ILogger<SaleForceService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("SaleForce service is starting.");
            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(1), TimeSpan.FromHours(1));

            DoWork(null);

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<HealMonitoringContext>();
                var manager = new InformationManager(dbContext);

                foreach (var connexion in dbContext.Connexion.ToList())
                {
                    if (connexion.platforme != nameof(Saleforce))
                        continue;

                    var data = new Process(connexion);

                    try
                    {
                        var record = data.Execute();
                        await manager.Replace(connexion.ID_User, nameof(Saleforce), record);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("SaleForce service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
