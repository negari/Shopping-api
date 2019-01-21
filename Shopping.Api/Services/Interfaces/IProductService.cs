using System.Collections.Generic;
using System.Threading.Tasks;
using Shopping.Api.Models;

namespace Shopping.Api.Services.Interfaces
{
    public interface IProductService
    {
        Task<IList<Product>> GetProducts();

        Task<IList<Product>> SortProducts(IList<Product> products, string sortOption);
    }
}
