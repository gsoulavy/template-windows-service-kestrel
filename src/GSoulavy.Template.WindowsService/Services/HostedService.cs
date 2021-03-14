namespace GSoulavy.Template.WindowsService.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Configurations;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class HostedService : IHostedService, IDisposable
    {
        private readonly ILogger<HostedService> _logger;
        private readonly IOptionsMonitor<HostedSettings> _optionsMonitor;
        private int _executionCount;
        private Timer? _timer;

        public HostedService(IOptionsMonitor<HostedSettings> optionsMonitor, ILogger<HostedService> logger)
        {
            _optionsMonitor = optionsMonitor;
            _logger = logger;
        }

        public void Dispose() => _timer?.Dispose();

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed {Name} Service running.", _optionsMonitor.CurrentValue.Name);
            _timer = new Timer(
                DoWork,
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(5)
            );

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed {Name} Service is stopping.", _optionsMonitor.CurrentValue.Name);

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref _executionCount);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}",
                count
            );
        }
    }
}
