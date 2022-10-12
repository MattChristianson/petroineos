using Polly;

namespace PPTR.Services.Abstractions
{
    public interface IPolicyProvider
    {
        AsyncPolicy GetRetryPolicy();
    }
}