using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Models;
using Shopping.Api.Services.Helpers;
using Shopping.Api.Services.Interfaces;

namespace Shopping.Api.Controllers.V1
{
    [Route("v1/sort")]
    public class ProductsSortController: ControllerBase
    {

        private readonly IProductService _productService;

        public ProductsSortController(IProductService productService)
        {
            _productService = productService;
        }
       
        public async Task<ActionResult<IList<Product>>> SortProducts(string sortOption)
        {
            try
            {
                if (string.IsNullOrEmpty(sortOption) || !sortOption.IsValidSortType())
                {
                    return BadRequest("sortOption should have valid value");
                }

                var products = await _productService.GetProducts();
                if (products == null || !products.Any())
                {
                    return NotFound("Could not find any product");
                }

                var result = await _productService.SortProducts(products, sortOption);
                return Ok(result);
            }

            catch (Exception ex)
            {
                //_logger.LogError("Internal server error", ex);
                return StatusCode(500);
            }


        }
    }
}
