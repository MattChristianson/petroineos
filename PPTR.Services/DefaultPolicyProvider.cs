using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using PPTR.Domain;
using PPTR.Services.Abstractions;
using Services;

namespace PPTR.Services
{
    public class DefaultPolicyProvider : IPolicyProvider
    {
        public readonly ReportOptions _reportOptions;
        private readonly ILogger<DefaultPolicyProvider> _logger;

        public DefaultPolicyProvider(IOptions<ReportOptions> reportOptions, ILogger<DefaultPolicyProvider> logger)
        {
            _reportOptions = reportOptions.Value;
            _logger = logger;
        }

        public AsyncPolicy GetRetryPolicy()
        {
            var timeoutPolicy = Policy.TimeoutAsync(TimeSpan.FromSeconds(_reportOptions.Timeout), onTimeoutAsync: async (context, timespan, task) =>
            {
                await Task.Run(() => _logger.LogError($"PowerService timed out after {_reportOptions.Timeout} seconds"));
            });

            var retryPolicy = Policy
                    .Handle<PowerServiceException>()
                    .WaitAndRetryAsync(
                    _reportOptions.RetryAttempts,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        if (retryCount < _reportOptions.RetryAttempts)
                        {
                            _logger.LogError(exception, $"PowerService encountered an exception and will retry. Current retry count: {retryCount}");
                        }
                        else
                        {
                            _logger.LogError(exception, $"PowerService has failed after {_reportOptions.RetryAttempts - 1} attempts. Last attempt.");
                        }
                    });

            return timeoutPolicy.WrapAsync(retryPolicy);
        }
    }
}
