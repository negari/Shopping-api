using System.Collections.Generic;

namespace Shopping.Api.Models.Trolley
{
    public class ProductSpecial
    {
        public IList<ProductQuantity> Quantities { get; set; }

        public decimal Total { get; set; }
    }
}
