using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPI_P3.Data.Models;
using TPI_P3.Services.Interfaces;

namespace TPI_P3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    /// VALIDAR SOLO Q USUARIOS AUTENTICADOS PUEDAN
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }

        [HttpGet("GetProductsId/{id}")]
        
        public IActionResult GetProductsId(int id)
        {
            return Ok(_productService.GetProductById(id));
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] ProductDto productDto)
        {
            try
            {
                var addedProduct = _productService.AddProduct(productDto);
                return CreatedAtAction("AddProduct", new { id = addedProduct.ProductId }, addedProduct);
            }
            catch (ArgumentException ex)
            {
                // Manejar el error de validación de colores o tallas que no existen
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteProductById/{id}")]
        public IActionResult DeleteProductById(int id)
        {
            _productService.DeleteProduct(id);
            return Ok();
        }

        [HttpPut("UpdateProductStatusById/{id}")]
        public IActionResult UpdateProductStatusById(int id)
        {
            _productService.UpdateProductStatusById(id);
            return Ok();
        }


    }
}