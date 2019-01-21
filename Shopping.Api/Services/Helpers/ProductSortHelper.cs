using System;
using System.Collections.Generic;
using System.Linq;


namespace Shopping.Api.Services.Helpers
{
    public static  class ProductSortHelper
    {

        /// <summary>
        /// Low to High Price
        /// </summary>
        public const string LowToHigh = "low";

        /// <summary>
        ///  High to Low Price
        /// </summary>
        public const string HighToLow = "high";

        /// <summary>
        ///  A - Z sort on the Name
        /// </summary>
        public const string Ascending = "ascending";

        /// <summary>
        ///  Z - A sort on the Name
        /// </summary>
        public const string Descending = "descending";

        /// <summary>
        /// order based on popularity in customers orders
        /// </summary>
        public const string Recommended = "recommended";

        /// <summary>
        /// List of valid sort options
        /// </summary>
        private static readonly IList<string> SortOptions = new List<string>() { LowToHigh, HighToLow, Ascending, Descending, Recommended };

        public static bool IsValidSortType(this string sortType)
        {
            return SortOptions.Contains(sortType, StringComparer.OrdinalIgnoreCase);
        }

    }
}
