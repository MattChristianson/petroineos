using Microsoft.Extensions.Options;
using PPTR.Domain;
using PPTR.Services.Abstractions;
using System.Timers;
using Timer = System.Timers.Timer;

namespace PPTR;

public class HostedReportService : IHostedService, IDisposable
{
    private readonly ILogger<HostedReportService> _logger;
    private readonly IIntraDayReportGenerator _intraDayReportGenerator;
    private Timer? _timer = null;
    private int executionCount = 0;
    private readonly ReportOptions _reportOptions;

    public HostedReportService(
        ILogger<HostedReportService> logger,
        IOptions<ReportOptions> reportOptions,
        IIntraDayReportGenerator intraDayReportGenerator)
    {
        _logger = logger;
        _intraDayReportGenerator = intraDayReportGenerator;
        _reportOptions = reportOptions.Value;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");
        _logger.LogInformation($"Tasks scheduled at intervals of {_reportOptions.Interval / 60} minutes");

        if (executionCount == 0)
        {
            RunReportAsync();
        }

        _timer = new Timer(_reportOptions.Interval * 1000);
        _timer.Elapsed += new ElapsedEventHandler((s, e) => RunReportAsync());
        _timer.Start();
        return Task.CompletedTask;
    }

    private async void RunReportAsync()
    {
        var count = Interlocked.Increment(ref executionCount);

        await _intraDayReportGenerator.GenerateReportAsync();

        _logger.LogInformation("Report service run complete. Count: {Count}", count);
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Report Service is stopping.");

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
