using PPTR.Domain;
using PPTR.Services.Abstractions;
using Services;

namespace PPTR.Services
{
    public class PositionAggrigator : IPositionAggrigator
    {
        private readonly ITimePeriodConverter _periodConverter;

        public PositionAggrigator(ITimePeriodConverter periodConverter)
        {
            _periodConverter = periodConverter;
        }

        public IEnumerable<AggregatedPosition> Aggregate(IEnumerable<PowerTrade> trades)
        {
            return trades.SelectMany(trds => trds.Periods)
                  .GroupBy(period => period.Period)
                  .Select(periodGrp => new AggregatedPosition()
                  {
                      Period = _periodConverter.ToTimePeriod(periodGrp.Key),
                      Volume = periodGrp.Sum(prd => prd.Volume)
                  });
        }
    }
}
