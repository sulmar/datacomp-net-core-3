using DataComp.Training.IServices;
using DataComp.Training.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataComp.Training.Api.Controllers
{
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServiceAsync productService;

        public ProductsController(IProductServiceAsync productService)
        {
            this.productService = productService;
        }

        //[HttpGet]
        //public IEnumerable<Product> Get()
        //{

        //}

        [HttpGet]
        public async Task<ActionResult<Product>> Get()
        {
            var products = await productService.GetAsync();

            return Ok(products);
        }

        // GET api/users/{guid}/products

        // ~ oznacza pominięcie prefiksu kontrolera 

        [HttpGet("~/api/users/{id:guid}/products")]
        public async Task<IActionResult> GetProducts(Guid id)
        {
            var products = await productService.GetByCustomerAsync(id);

            return Ok(products);
        }


        // GET api/users/{guid}/products/{productId}
    }
}
  