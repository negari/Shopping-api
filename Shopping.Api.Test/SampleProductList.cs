using System;
using System.Collections.Generic;
using System.Text;
using Shopping.Api.Models;

namespace Shopping.Api.Test
{
    public static class SampleProductList
    {

        public static  List<Product> OrginalProducts = new List<Product>()
        {
            new Product() {Name = "C", Price = 1},
            new Product() {Name = "bA", Price = 3},
            new Product() {Name = "b", Price = 2}
        };

        public static List<Product> SortedByLowerPriceProducts = new List<Product>()
        {
            new Product() {Name = "C", Price = 1},
            new Product() {Name = "b", Price = 2},
            new Product() {Name = "bA", Price = 3}
        };

        public static IList<Product> SortedByHigherPriceProducts = new List<Product>()
        {
            new Product() {Name = "bA", Price = 3},
            new Product() {Name = "b", Price = 2},
            new Product() {Name = "C", Price = 1}
        };

        public static IList<Product> SortedByAscendingProducts = new List<Product>()
        {
            new Product() {Name = "b", Price = 2},
            new Product() {Name = "bA", Price = 3},
            new Product() {Name = "C", Price = 1}
        };

        public static IList<Product> SortedByDescendingProducts = new List<Product>()
        {
            new Product() {Name = "C", Price = 1},
            new Product() {Name = "bA", Price = 3},
            new Product() {Name = "b", Price = 2}
        };


    }
}
