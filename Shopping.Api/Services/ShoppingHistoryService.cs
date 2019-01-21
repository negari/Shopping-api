using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Shopping.Api.Models.Response;
using Shopping.Api.Options;
using Shopping.Api.Services.Interfaces;

namespace Shopping.Api.Services
{
    public class ShoppingHistoryService: IShoppingHistoryService
    {

        private readonly IApiClient _apiClient;
        private readonly ResourceSettings _resourceSettings;


        public ShoppingHistoryService(IApiClient apiClient,IOptions<ResourceSettings> resourceSettings)
        {
            _apiClient = apiClient;
           _resourceSettings = resourceSettings.Value;
        }

        public async Task<IList<string>> GetShoppingHistorySortedByQuantity()
        {
            var shoppingHistoryDic = new Dictionary<string, decimal>();
            var shoppingHistoryList= await _apiClient.GetAsync<IList<ShopperHistoryResponse>>(_resourceSettings.ShoppingHistory);
            if (shoppingHistoryList == null || !shoppingHistoryList.Any())
                return null;

            foreach (var shoppingHistory in shoppingHistoryList)
            {
                foreach(var product in shoppingHistory.Products)
                    if (shoppingHistoryDic.ContainsKey(product.Name))
                        shoppingHistoryDic[product.Name] += product.Quantity;
                    else
                    {
                        shoppingHistoryDic.Add(product.Name,product.Quantity);
                    }
            }
            var result = shoppingHistoryDic.OrderByDescending(i => i.Value).Select(x=>x.Key).ToList();
            return result;
        }

        
    }
}
