using PPTR.Services.Abstractions;
using System;

namespace PPTR.Services.Utility
{
    public class DateTimeOffsetProvider : IDateTimeOffsetProvider
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.Now;
        }
    }
}
