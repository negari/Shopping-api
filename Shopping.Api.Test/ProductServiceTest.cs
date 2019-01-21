using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Shopping.Api.Options;
using Xunit;
using Shopping.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using Shopping.Api.Models;
using Shopping.Api.Services;

namespace Shopping.Api.Test
{
    public class ProductServiceTest
    {

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<IShoppingHistoryService> _shoppingHistoryService = new Mock<IShoppingHistoryService>();
        private readonly IOptions<ResourceSettings> _Settings = Microsoft.Extensions.Options.Options.Create(new ResourceSettings()  { BaseUrl = "http://something" });

        [Theory]
        [InlineData("low")]
        [InlineData("Low")]
        public async Task SortProducts_LowSortOption_ShouldSortBasedonProductsPriceLowToHigh(string sortOption)
        {
            var service = new ProductService(_apiClient.Object, _shoppingHistoryService.Object , _Settings);
            var result = (await service.SortProducts(SampleProductList.OrginalProducts, sortOption)).ToList();
            for (var i=0;i<result.Count ;i++ )
            Assert.Equal(SampleProductList.SortedByLowerPriceProducts[i].Name , result[i].Name);
        }

        [Theory]
        [InlineData("high")]
        [InlineData("HIGH")]
        public async Task SortProducts_HighSortOption_ShouldSortBasedonProductsPriceHighToLow(string sortOption)
        {
            var service = new ProductService(_apiClient.Object, _shoppingHistoryService.Object, _Settings);
            var result = (await service.SortProducts(SampleProductList.OrginalProducts, sortOption)).ToList();
            for (var i = 0; i < result.Count; i++)
                Assert.Equal(SampleProductList.SortedByHigherPriceProducts[i].Name, result[i].Name);
        }

        [Theory]
        [InlineData("Ascending")]
        [InlineData("AscendING")]
        public async Task SortProducts_AscendingSortOption_ShouldSortBasedonProductsNameAscending(string sortOption)
        {
            var service = new ProductService(_apiClient.Object, _shoppingHistoryService.Object, _Settings);
            var result = (await service.SortProducts(SampleProductList.OrginalProducts, sortOption)).ToList();
            for (var i = 0; i < result.Count; i++)
                Assert.Equal(SampleProductList.SortedByAscendingProducts[i].Name, result[i].Name);
        }


        [Theory]
        [InlineData("Descending")]
        [InlineData("descending")]
        public async Task SortProducts_DescendingSortOption_ShouldSortBasedonProductsNameDescending(string sortOption)
        {
            var service = new ProductService(_apiClient.Object, _shoppingHistoryService.Object, _Settings);
            var result = (await service.SortProducts(SampleProductList.OrginalProducts, sortOption)).ToList();
            for (var i = 0; i < result.Count; i++)
                Assert.Equal(SampleProductList.SortedByDescendingProducts[i].Name, result[i].Name);
        }

        [Fact]
        public async Task SortProducts_RecommendedSortOption_ShouldSortBasedonShoppingHistoryResultAndOtherProductShouldBeAtTheEnd()
        {
            _shoppingHistoryService.Setup(x => x.GetShoppingHistorySortedByQuantity())
                .ReturnsAsync(new List<string>{ "bA", "Other1", "b", "Other2" });
            var service = new ProductService(_apiClient.Object, _shoppingHistoryService.Object, _Settings);
            var result = (await service.SortProducts(SampleProductList.OrginalProducts, "Recommended")).ToList();
            Assert.Equal("bA", result[0].Name);
            Assert.Equal("b", result[1].Name);
            Assert.Equal("C", result[2].Name);
        }

        [Fact]
        public async Task SortProducts_RecommendedSortOptionWihNoShoppingHistory_ShouldReturnInputProducts()
        {
            _shoppingHistoryService.Setup(x => x.GetShoppingHistorySortedByQuantity())
                .ReturnsAsync((List<string>) null);
            var service = new ProductService(_apiClient.Object, _shoppingHistoryService.Object, _Settings);
            var result = (await service.SortProducts(SampleProductList.OrginalProducts, "Recommended")).ToList();
            Assert.Equal(SampleProductList.OrginalProducts[0].Name, result[0].Name);
            Assert.Equal(SampleProductList.OrginalProducts[1].Name, result[1].Name);
            Assert.Equal(SampleProductList.OrginalProducts[2].Name , result[2].Name);
        }
    }
}
