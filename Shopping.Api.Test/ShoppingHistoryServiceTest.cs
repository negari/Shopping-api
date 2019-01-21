using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Shopping.Api.Models.Response;
using Shopping.Api.Options;
using Shopping.Api.Services;
using Shopping.Api.Services.Interfaces;
using Xunit;

namespace Shopping.Api.Test
{
    public class ShoppingHistoryServiceTest
    {
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly IOptions<ResourceSettings> _Settings = Microsoft.Extensions.Options.Options.Create(new ResourceSettings() { BaseUrl = "http://something" });

       [Fact]
        public async Task GetShoppingHistorySortedByQuantity_shouldReturnProductsOrderByQuantityDescending()
        {
            string json = File.ReadAllText("ShoppingHistory.json");
            _apiClient.Setup(x => x.GetAsync<IList<ShopperHistoryResponse>>(It.IsAny<string>())).ReturnsAsync(JsonConvert.DeserializeObject<List<ShopperHistoryResponse>>(json));
            var service = new ShoppingHistoryService(_apiClient.Object, _Settings);
            var result =await service.GetShoppingHistorySortedByQuantity();
            Assert.Equal(4, result.Count);
            Assert.Equal("Test Product A", result[0]);
            Assert.Equal("Test Product B", result[1]);
            Assert.Equal("Test Product F", result[2]);
            Assert.Equal("Test Product C", result[3]);

        }
    }
}
