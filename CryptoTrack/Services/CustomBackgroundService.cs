public class CustomBackgroundService : IHostedService, IDisposable
{
    private readonly ILogger<CustomBackgroundService> _logger;
    private Timer? _timer;
    private readonly Action _job;

    public CustomBackgroundService(ILogger<CustomBackgroundService> logger, Action job)
    {
        _logger = logger;
        _job = job;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service starting...");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(2));

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        _logger.LogInformation("Service running at: {time}", DateTimeOffset.Now);

        _job();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service stopping...");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
