using CsvHelper;
using PPTR.Domain;
using PPTR.Services.Abstractions;
using System.Globalization;
namespace PPTR.Services
{
    public class IntraDayCsvReportWriter : IIntraDayCsvReportWriter
    {
        private readonly IReportPathProvider _pathProvider;

        public IntraDayCsvReportWriter(IReportPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public void Write(IEnumerable<AggregatedPosition> positionAggregations, DateTimeOffset date)
        {
            var fullPath = _pathProvider.GetPath(date);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            using (var writer = new StreamWriter(fullPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteHeader<AggregatedPosition>();
                csv.NextRecord();
                csv.WriteRecords(positionAggregations);
            }
        }
    }
}
