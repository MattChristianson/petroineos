using NUnit.Framework;
using PPTR.Services;
using PPTR.Services.Utility;
using PPTR.PowerTradersReporting.Tests.TestData;
using Services;
using CollectionAssert = NUnit.Framework.CollectionAssert;

namespace PowerTradersIntraDayReport.Tests
{
    [TestFixture]
    public class PositionAggrigatorTests
    {
        [Test]
        public void PositionAggrigator_aggregates_trades()
        {
            var aggregator = new PositionAggrigator(new TimePeriodConverter());

            //Trades as per Challenge Requirements document
            var trades = new List<PowerTrade>()
            {
                PowerTradePositions.PowerTrade_1(),
                PowerTradePositions.PowerTrade_2()
            };

            var aggregatedPositions = aggregator.Aggregate(trades);

            //Results as per Challenge Requirements document
            var expected = PowerTradePositions.PowerTrade_Aggregated();

            CollectionAssert.AreEqual(expected.Select(ap => ap.Period), aggregatedPositions.Select(ap => ap.Period));
            CollectionAssert.AreEqual(expected.Select(ap => ap.Volume), aggregatedPositions.Select(ap => ap.Volume));
        }
    }
}