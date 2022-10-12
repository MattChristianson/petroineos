using Microsoft.Extensions.Options;
using PPTR.Domain;
using PPTR.Services.Abstractions;

namespace PPTR.Services
{
    public class ReportPathProvider : IReportPathProvider
    {
        private readonly string _filePath;

        public ReportPathProvider(IOptions<ReportOptions> reportOptions)
        {
            if (string.IsNullOrEmpty(reportOptions.Value.ReportsPath))
                throw new ArgumentException("Reports file path is not specified");

            _filePath = reportOptions.Value.ReportsPath;
        }

        public string GetPath(DateTimeOffset date)
        {
            return Path.Combine(_filePath, $"PowerPosition_{date:yyyyMMdd_HHmm}.csv");
        }
    }
}
