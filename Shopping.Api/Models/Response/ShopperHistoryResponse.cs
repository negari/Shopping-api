using System.Collections.Generic;

namespace Shopping.Api.Models.Response
{
    public class ShopperHistoryResponse
    {
        /// <summary>
        /// Customer Identification
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Shopping history for customer
        /// </summary>
        public IList<Product> Products { get; set; }
    }
}
