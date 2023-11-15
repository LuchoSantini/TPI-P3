using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TPI_P3.Data;
using TPI_P3.Data.Entities;
using TPI_P3.Data.Models;
using TPI_P3.Services.Implementations;
using TPI_P3.Services.Interfaces;

namespace TPI_P3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]


    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly TPIContext _context;

        public ProductController(IProductService productService, TPIContext context)
        {
            _productService = productService;
            _context = context;
        }
        
        [HttpGet("GetProducts")]
        public IActionResult GetProducts() // ver si el estado del producto es false no mostrarlo
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Client" || role == "Admin" ) 
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
                foreach (var colourId in productDto.ColourId)
                {
                    var existingColour = _context.Colours.FirstOrDefault(c => c.Id == colourId);
                    if(existingColour == null)
                    {
                        return BadRequest("El Id del color no existe");
                    }
                }
                foreach (var sizeId in productDto.SizeId)
                {
                    var existingSize = _context.Colours.FirstOrDefault(s => s.Id == sizeId);
                    if (existingSize == null)
                    {
                        return BadRequest("El Id del talle no existe");
                    }
                }

                var addedProduct = _productService.AddProduct(productDto);
                    return CreatedAtAction("AddProduct", new { id = addedProduct.ProductId }, addedProduct); 
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