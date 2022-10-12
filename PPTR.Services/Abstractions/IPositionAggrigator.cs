using PPTR.Domain;
using Services;

namespace PPTR.Services.Abstractions
{
    public interface IPositionAggrigator
    {
        IEnumerable<AggregatedPosition> Aggregate(IEnumerable<PowerTrade> trades);
    }
}