
using System.Collections.Generic;

namespace Shopping.Api.Models.Trolley
{
    public class Trolley
    {
        public IList<ProductPrice> Products { get; set; }

        public IList<ProductSpecial> Specials { get; set; }

        public IList<ProductQuantity> Quantities { get; set; }

    }
}
