namespace PPTR.Services.Abstractions
{
    public interface IIntraDayReportGenerator
    {
        Task GenerateReportAsync();
    }
}