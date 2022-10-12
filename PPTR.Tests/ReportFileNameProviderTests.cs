using NUnit.Framework;
using Microsoft.Extensions.Options;
using PPTR.PowerTradersReporting;
using PPTR.Services;
using Assert = NUnit.Framework.Assert;
using PPTR.Domain;

namespace PowerTradersIntraDayReport.Tests
{
    [TestFixture]
    public class ReportFileNameProviderTests
    {
        [Test]
        public void ReportFileNameProvider_Generates_Correct_Full_Path()
        {
            var options = Options.Create(new ReportOptions
            {
                ReportsPath = "C:/temp/reports/"
            });
            var fileNameProvider = new ReportPathProvider(options);
            var path = fileNameProvider.GetPath(new DateTimeOffset(2021, 11, 17, 10, 55, 0, TimeSpan.Zero));

            var expectedFilePath = "C:/temp/reports/PowerPosition_20211117_1055.csv";

            Assert.AreEqual(expectedFilePath, path);
        }
    }
}