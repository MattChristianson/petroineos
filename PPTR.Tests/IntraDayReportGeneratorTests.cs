using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using PPTR.Domain;
using PPTR.Services;
using PPTR.Services.Abstractions;
using PPTR.PowerTradersReporting.Tests.TestData;
using Services;

namespace PPTR.Tests
{
    [TestFixture]
    public class IntraDayReportGeneratorTests
    {
        private IPowerService _powerSeriveMock;
        private IPositionAggrigator _positionAggrigatorMock;
        private ILogger<IntraDayReportGenerator> _loggerMock;
        private IDateTimeOffsetProvider _dateProviderMock;
        private IIntraDayCsvReportWriter _csvWriterMock;
        private IEnumerable<PowerTrade> _testTrades;
        private IEnumerable<AggregatedPosition> _aggregatedPositions;
        private IPolicyProvider _policyProvider;


        private DateTimeOffset _runTime;

        [SetUp]
        public void Init()
        {
            _testTrades = new List<PowerTrade>()
            {
                PowerTradePositions.PowerTrade_1(),
                PowerTradePositions.PowerTrade_2()
            };
            _runTime = new DateTimeOffset(2021, 11, 17, 10, 55, 0, TimeSpan.Zero);
            _aggregatedPositions = (IEnumerable<AggregatedPosition>)PowerTradePositions.PowerTrade_Aggregated();

            _powerSeriveMock = Substitute.For<IPowerService>();
            _powerSeriveMock.GetTradesAsync(Arg.Any<DateTime>()).Returns(_testTrades);

            _positionAggrigatorMock = Substitute.For<IPositionAggrigator>();
            _positionAggrigatorMock.Aggregate(Arg.Any<IEnumerable<PowerTrade>>()).Returns(_aggregatedPositions);


            _dateProviderMock = Substitute.For<IDateTimeOffsetProvider>();
            _dateProviderMock.Now().Returns(_runTime);

            _loggerMock = Substitute.For<ILogger<IntraDayReportGenerator>>();
            _csvWriterMock = Substitute.For<IIntraDayCsvReportWriter>();


            var options = Options.Create(new ReportOptions
            {
                Interval = 1,
                Timeout = 1
            });

            _policyProvider = new DefaultPolicyProvider(options, Substitute.For<ILogger<DefaultPolicyProvider>>());
        }

        [Test]
        public async Task IntraDayReportGenerator_calls_PowerService_with_current_run_time()
        {
            var reportGenerator = new IntraDayReportGenerator(
                _powerSeriveMock,
                _positionAggrigatorMock,
                _loggerMock,
                _dateProviderMock,
                _csvWriterMock,
                _policyProvider);

            await reportGenerator.GenerateReportAsync();

            _powerSeriveMock.Received(1).GetTradesAsync(Arg.Is<DateTime>(dt => dt.Ticks == _runTime.Date.Ticks));
        }

        [Test]
        public async Task IntraDayReportGenerator_calls_aggrigator_with_trades()
        {
            var reportGenerator = new IntraDayReportGenerator(_powerSeriveMock, _positionAggrigatorMock, _loggerMock, _dateProviderMock, _csvWriterMock, _policyProvider);

            await reportGenerator.GenerateReportAsync();

            _positionAggrigatorMock.Received(1).Aggregate(_testTrades);
        }

        [Test]
        public async Task IntraDayReportGenerator_calls_IntraDayCsvReportWriter_with_current_run_time_and_aggregations()
        {
            var reportGenerator = new IntraDayReportGenerator(_powerSeriveMock, _positionAggrigatorMock, _loggerMock, _dateProviderMock, _csvWriterMock, _policyProvider);

            await reportGenerator.GenerateReportAsync();

            _csvWriterMock.Received(1).Write(_aggregatedPositions, _runTime);
        }
    }
}
