using PPTR.Services.Abstractions;

namespace PPTR.Services.Utility
{
    public class TimePeriodConverter : ITimePeriodConverter
    {
        public string ToTimePeriod(int period)
        {
            return DateTime.Today.AddHours(period - 2).ToString("HH:mm");
        }
    }
}
