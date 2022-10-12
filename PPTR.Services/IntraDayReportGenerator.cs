using Microsoft.Extensions.Logging;
using PPTR.Services.Abstractions;
using Services;

namespace PPTR.Services
{
    public class IntraDayReportGenerator : IIntraDayReportGenerator
    {
        private readonly IPowerService _powerService;
        private readonly IPositionAggrigator _positionAggrigator;
        private readonly ILogger<IntraDayReportGenerator> _logger;
        private readonly IDateTimeOffsetProvider _dateProvider;
        private readonly IIntraDayCsvReportWriter _csvWriter;
        private readonly IPolicyProvider _policyProvider;

        public IntraDayReportGenerator(
            IPowerService powerService,
            IPositionAggrigator positionAggrigator,
            ILogger<IntraDayReportGenerator> logger,
            IDateTimeOffsetProvider dateProvider,
            IIntraDayCsvReportWriter csvWriter,
            IPolicyProvider policyProvider)
        {
            _powerService = powerService;
            _positionAggrigator = positionAggrigator;
            _logger = logger;
            _dateProvider = dateProvider;
            _csvWriter = csvWriter;
            _policyProvider = policyProvider;
        }

        public async Task GenerateReportAsync()
        {
            try
            {
                var runTime = _dateProvider.Now();
                _logger.LogInformation($"Generating intra day report at {runTime: dd-MM-yyyy HH:mm:ss}");

                var trades = await GetTradesAsync(runTime);
                var positionAggregations = _positionAggrigator.Aggregate(trades);
                _csvWriter.Write(positionAggregations, runTime);
                _logger.LogInformation("Report written to csv");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception was encounted while running the intra day report");
            }
        }

        public async Task<IEnumerable<PowerTrade>> GetTradesAsync(DateTimeOffset date)
        {
            _logger.LogInformation("Requesting trades");

            var policyResult = await _policyProvider
                .GetRetryPolicy()
                .ExecuteAndCaptureAsync(async () => await _powerService.GetTradesAsync(date.Date));

            var trades = policyResult.Result;

            _logger.LogInformation($"{trades.Count()} trades returned");
            return trades;
        }
    }
}
