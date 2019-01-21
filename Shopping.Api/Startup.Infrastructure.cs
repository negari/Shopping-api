using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shopping.Api.Options;
using Shopping.Api.Services;
using Shopping.Api.Services.Interfaces;

namespace Shopping.Api
{
    public static class StartupInfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            HttpMessageHandler httpPipeline = null)
        {
            services.TryAddSingleton<IUserService, UserService>();

            services.TryAddSingleton<IProductService, ProductService>();

            services.TryAddSingleton<IShoppingHistoryService, ShoppingHistoryService>();

            services.TryAddSingleton<ITrolleyCalculatorService, TrolleyCalculatorService>();

            services.TryAddSingleton<IApiClient , ApiClient>();

            services.AddOptions()
                .Configure<UserSettings>(options => configuration.GetSection("UserSettings").Bind(options));

            services.AddOptions()
                .Configure<ResourceSettings>(options => configuration.GetSection("ResourceSettings").Bind(options));

            return services;
        }
    }
}
