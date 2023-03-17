using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;

namespace Shopbridge_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductsController> logger;
        public ProductsController(IProductService _productService, ILogger<ProductsController> _logger)
        {
            this.productService = _productService;
            this.logger = _logger;
        }

       
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return Ok(await productService.GetAllProducts());
        }

        
        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetProduct(int id)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            return Ok(await productService.GetProduct(id));
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Product_Id)
                return BadRequest();

            if (!ProductExists(id))
                return NotFound();

            product.ModificationDate = DateTime.Now;

            await productService.UpdateProductAsync(id, product);

            return Ok();
        }

        
        [HttpPost]
        public async Task<IActionResult> PostProduct(Product product)
        {
            if (ProductExists(product.Product_Id))
            {
                return BadRequest(product);
            }

            product.CreationDate = DateTime.Now;
            product.ModificationDate = DateTime.Now;

            return Ok(await productService.AddProductAsync(product));
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }

            await productService.DeleteProductAsync(id);
            return Ok();
        }

        private bool ProductExists(int id)
        {
            return productService.FindProduct(id).Result;
        }
    }
}
