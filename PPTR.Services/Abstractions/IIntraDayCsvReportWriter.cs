using PPTR.Domain;

namespace PPTR.Services.Abstractions
{
    public interface IIntraDayCsvReportWriter
    {
        void Write(IEnumerable<AggregatedPosition> positionAggregations, DateTimeOffset date);
    }
}