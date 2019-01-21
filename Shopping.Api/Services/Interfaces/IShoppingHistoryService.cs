using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Api.Services.Interfaces
{
    public interface IShoppingHistoryService
    {
        Task<IList<string>> GetShoppingHistorySortedByQuantity();
    }
}
