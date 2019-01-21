
using System.Threading.Tasks;

namespace Shopping.Api.Services.Interfaces
{
    public interface IApiClient
    {
        Task<TResponse> GetAsync<TResponse>(string relativePath);

        Task<TResponse> Post<TRequest, TResponse>(TRequest request, string relativePath);
    }
}
