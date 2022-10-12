using PPTR.Domain;
using Services;

namespace PPTR.PowerTradersReporting.Tests.TestData
{
    public static class PowerTradePositions
    {
        public static PowerTrade PowerTrade_1()
        {
            var trade = PowerTrade.Create(DateTime.Now, 12);

            trade.Periods[0].Period = 1;
            trade.Periods[0].Volume = 100;

            trade.Periods[1].Period = 2;
            trade.Periods[1].Volume = 100;

            trade.Periods[2].Period = 3;
            trade.Periods[2].Volume = 100;

            trade.Periods[3].Period = 4;
            trade.Periods[3].Volume = 100;

            trade.Periods[4].Period = 5;
            trade.Periods[4].Volume = 100;

            trade.Periods[5].Period = 6;
            trade.Periods[5].Volume = 100;

            trade.Periods[6].Period = 7;
            trade.Periods[6].Volume = 100;

            trade.Periods[7].Period = 8;
            trade.Periods[7].Volume = 100;

            trade.Periods[8].Period = 9;
            trade.Periods[8].Volume = 100;

            trade.Periods[9].Period = 10;
            trade.Periods[9].Volume = 100;

            trade.Periods[10].Period = 11;
            trade.Periods[10].Volume = 100;

            trade.Periods[11].Period = 12;
            trade.Periods[11].Volume = 100;

            return trade;
        }

        public static PowerTrade PowerTrade_2()
        {
            var trade = PowerTrade.Create(DateTime.Now, 12);

            trade.Periods[0].Period = 1;
            trade.Periods[0].Volume = 50;

            trade.Periods[1].Period = 2;
            trade.Periods[1].Volume = 50;

            trade.Periods[2].Period = 3;
            trade.Periods[2].Volume = 50;

            trade.Periods[3].Period = 4;
            trade.Periods[3].Volume = 50;

            trade.Periods[4].Period = 5;
            trade.Periods[4].Volume = 50;

            trade.Periods[5].Period = 6;
            trade.Periods[5].Volume = 50;

            trade.Periods[6].Period = 7;
            trade.Periods[6].Volume = 50;

            trade.Periods[7].Period = 8;
            trade.Periods[7].Volume = 50;

            trade.Periods[8].Period = 9;
            trade.Periods[8].Volume = 50;

            trade.Periods[9].Period = 10;
            trade.Periods[9].Volume = 50;

            trade.Periods[10].Period = 11;
            trade.Periods[10].Volume = 50;

            trade.Periods[11].Period = 12;
            trade.Periods[11].Volume = -20;

            return trade;
        }

        public static IEnumerable<AggregatedPosition> PowerTrade_Aggregated()
        {
            return new List<AggregatedPosition>()
            {
                new AggregatedPosition("23:00", 150),
                new AggregatedPosition("00:00", 150),
                new AggregatedPosition("01:00", 150),
                new AggregatedPosition("02:00", 150),
                new AggregatedPosition("03:00", 150),
                new AggregatedPosition("04:00", 150),
                new AggregatedPosition("05:00", 150),
                new AggregatedPosition("06:00", 150),
                new AggregatedPosition("07:00", 150),
                new AggregatedPosition("08:00", 150),
                new AggregatedPosition("09:00", 150),
                new AggregatedPosition("10:00", 80)
        };
        }
    }
}
