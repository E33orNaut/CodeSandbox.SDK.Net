using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeSandbox.SDK.Net.Interfaces
{
    public interface IApiClient : IDisposable
    {
        Task<T> GetAsync<T>(string path, CancellationToken cancellationToken = default);
        Task<T> PostAsync<T>(string path, object payload, CancellationToken cancellationToken = default);
        Task<T> PutAsync<T>(string path, object payload, CancellationToken cancellationToken = default);
        Task DeleteAsync(string path, CancellationToken cancellationToken = default);
    }
}
