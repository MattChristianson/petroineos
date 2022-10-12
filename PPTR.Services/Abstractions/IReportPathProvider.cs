using System;

namespace PPTR.Services.Abstractions
{
    public interface IReportPathProvider
    {
        string GetPath(DateTimeOffset date);
    }
}