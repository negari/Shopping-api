using System.Threading.Tasks;
using Shopping.Api.Models.Trolley;

namespace Shopping.Api.Services.Interfaces
{
    public interface ITrolleyCalculatorService
    {
         Task<decimal?> Calculate(Trolley trolley);
    }
}
