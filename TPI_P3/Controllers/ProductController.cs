using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Timers;
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
        public IActionResult GetProducts()
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Client" || role == "Admin")
            {
                var productList = _productService.GetProducts().Where(p => p.Status != false).ToList();
                return Ok(productList);
            }
            return Forbid();
        }

        [HttpGet("GetProductsId/{id}")]

        public IActionResult GetProductsId(int id)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Client" || role == "Admin")
            {
                var productToGet = _context.Products.FirstOrDefault(p => p.ProductId == id);
                if (productToGet == null)
                {
                    return NotFound($"El producto de ID {id} no se ha encontrado.");
                }
                return Ok(_productService.GetProductById(id));
            }
            return Forbid();
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] ProductDto productDto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                if (productDto.Description == "string" || string.IsNullOrEmpty(productDto.Description))
                {
                    return BadRequest("La descripción del producto no puede estar vacía.");
                }

                if (productDto.Price <= 0)
                {
                    return BadRequest("El precio del producto debe ser mayor que cero.");
                }

                foreach (var colourId in productDto.ColourId)
                {
                    var existingColour = _context.Colours.FirstOrDefault(c => c.Id == colourId);
                    if (existingColour == null)
                    {
                        return BadRequest($"El ID del color {colourId} no existe.");
                    }
                }

                foreach (var sizeId in productDto.SizeId)
                {
                    var existingSize = _context.Sizes.FirstOrDefault(s => s.Id == sizeId);
                    if (existingSize == null)
                    {
                        return BadRequest($"El ID del tamaño {sizeId} no existe.");
                    }
                }

                var addedProduct = _productService.AddProduct(productDto);
                return CreatedAtAction("AddProduct", new { id = addedProduct.ProductId }, addedProduct);
            }
            return Forbid();
        }


        [HttpDelete("DeleteProductById/{id}")] //Dar de baja
        public IActionResult DeleteProductById(int id)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                var productToDelete = _context.Products.FirstOrDefault(p => p.ProductId == id);
                if (productToDelete == null)
                {
                    return NotFound($"El producto de ID {id} no se ha encontrado.");
                }

                _productService.DeleteProduct(id);
                return Ok($"El producto con el ID: {id} se ha eliminado correctamente");
            }
            return Forbid();
        }

        [HttpPut("UpdateProductStatusById/{id}")] //Dar de alta
        public IActionResult UpdateProductStatusById(int id)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                var productToUpdate = _context.Products.FirstOrDefault(p => p.ProductId == id);
                if (productToUpdate == null)
                {
                    return NotFound($"El producto de ID {id} no se ha encontrado.");
                }
                _productService.UpdateProductStatusById(id);

                return Ok($"El producto con el ID: {id} se ha dado de alta nuevamente");

            }
            return Forbid();
        }

        [HttpPut("EditProductById/{id}")]
        public IActionResult EditProductById(int id, [FromBody] ProductToEditDto productToEditDto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role == "Admin")
            {
                var productToEdit = _context.Products.Include(p => p.Colours).Include(p => p.Sizes).FirstOrDefault(p => p.ProductId == id);

                if (productToEdit == null)
                {
                    return NotFound($"El producto de ID {id} no se ha encontrado.");
                }
                //Validaciones para evitar que se actualicen por cosas por defecto
                if (productToEditDto.Description != "string" || !string.IsNullOrEmpty(productToEdit.Description))
                {
                    productToEdit.Description = productToEditDto.Description;
                }

                if (productToEditDto.Price > 0)
                {
                    productToEdit.Price = productToEditDto.Price;
                }

                if (productToEditDto.ColourIds != null && productToEditDto.ColourIds.Any() && productToEditDto.ColourIds.First() != 0)
                {
                    productToEdit.Colours = _context.Colours.Where(c => productToEditDto.ColourIds.Contains(c.Id)).ToList();
                }

                if (productToEditDto.SizeIds != null && productToEditDto.SizeIds.Any() && productToEditDto.SizeIds.First() != 0)
                {
                    productToEdit.Sizes = _context.Sizes.Where(s => productToEditDto.SizeIds.Contains(s.Id)).ToList();
                }

                _context.SaveChanges();
                return Ok($"El producto con el ID: {id} se ha actualizado correctamente.");
            }
            return Forbid();
        }

    }
}