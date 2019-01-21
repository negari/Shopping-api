using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Shopping.Api.Models;
using Shopping.Api.Options;
using Shopping.Api.Services.Helpers;
using Shopping.Api.Services.Interfaces;

namespace Shopping.Api.Services
{
    public class ProductService : IProductService
    {

        private readonly IApiClient _apiClient;
       private readonly ResourceSettings _resourceSettings;
        private readonly IShoppingHistoryService  _shoppingHistoryService ;


        public ProductService(IApiClient apiClient , IShoppingHistoryService shoppingHistoryService , IOptions<ResourceSettings> resourceSettings)
        {
            _apiClient = apiClient;
            _shoppingHistoryService = shoppingHistoryService;
            _resourceSettings = resourceSettings.Value;
        }

        public async Task<IList<Product>> GetProducts()
        {
            return await _apiClient.GetAsync<IList<Product>>(_resourceSettings.ProductResource);
        }

        public async Task<IList<Product>> SortProducts(IList<Product> products , string sortOption)
        {
            switch (sortOption.ToLower())
            {
                case ProductSortHelper.LowToHigh:
                     return products?.OrderBy(x => x.Price).ToList();
                case ProductSortHelper.HighToLow:
                    return products?.OrderByDescending(x => x.Price).ToList();
                case ProductSortHelper.Ascending:
                    return products?.OrderBy(x => x.Name).ToList();
                case ProductSortHelper.Descending:
                    return products?.OrderByDescending(x => x.Name).ToList();
                case ProductSortHelper.Recommended:
                    return await SortProductsRecommendedByShoppingHistory(products);
            }
            return products;

        }

        public async Task<IList<Product>> SortProductsRecommendedByShoppingHistory(IList<Product> products)
        {
            var productsDic = products.ToDictionary(x=>x.Name,x=>new OrderedProduct(){Product =x,Order=int.MaxValue});
            var sortedShoopingHistoryProductNames = await _shoppingHistoryService.GetShoppingHistorySortedByQuantity();
            if (sortedShoopingHistoryProductNames == null || !sortedShoopingHistoryProductNames.Any())
                return products;
            for (int i = 0; i < sortedShoopingHistoryProductNames.Count;i++)
            {
                if (productsDic.ContainsKey(sortedShoopingHistoryProductNames[i]))
                { productsDic[sortedShoopingHistoryProductNames[i]].Order = i;}
            }
           return productsDic.OrderBy(i => i.Value.Order).Select(x => x.Value.Product).ToList();
        }
    }
}
