using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Shopping.Api.Models.Trolley;
using Shopping.Api.Options;
using Shopping.Api.Services.Interfaces;

namespace Shopping.Api.Services
{
    public class TrolleyCalculatorService: ITrolleyCalculatorService
    {
        private readonly IApiClient _apiClient;
        private readonly ResourceSettings _resourceSettings;

        public TrolleyCalculatorService(IApiClient apiClient, IOptions<ResourceSettings> resourceSettings)
        {
            _apiClient = apiClient;
            _resourceSettings = resourceSettings.Value;
        }

        public async Task<decimal?> Calculate(Trolley trolley)
        {
            return await _apiClient.Post<Trolley, decimal?>(trolley, _resourceSettings.TrolleyResource);
        }

    }
}
