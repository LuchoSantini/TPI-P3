using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TPI_P3.Data.Models;
using TPI_P3.Services.Implementations;
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
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Client" || role == "Admin") 
            {
                return Ok(_productService.GetProducts());
            }
            return Forbid();
        }

        [HttpGet("GetProductsId/{id}")]
        
        public IActionResult GetProductsId(int id)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Client" || role == "Admin") 
            {
                return Ok(_productService.GetProductById(id));
            }
            return Forbid();
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] ProductDto productDto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Client" || role == "Admin")
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
            return Forbid();
        }

        [HttpDelete("DeleteProductById/{id}")]
        public IActionResult DeleteProductById(int id)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Client" || role == "Admin")
            {
                _productService.DeleteProduct(id);
                return Ok();
            }
            return Forbid();
                
            
        }

        [HttpPut("UpdateProductStatusById/{id}")]
        public IActionResult UpdateProductStatusById(int id)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Client" || role == "Admin")
            {
                _productService.UpdateProductStatusById(id);
                return Ok();
            }
            return Forbid();
        }


    }
}