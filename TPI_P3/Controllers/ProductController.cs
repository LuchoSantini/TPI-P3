using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPI_P3.Data.ProductDto;
using TPI_P3.Services.Interfaces;

namespace TPI_P3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }
        [HttpGet("{id}")]
        public IActionResult GetProductsId(int id)
        {
            return Ok(_productService.GetProductById(id));
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody]ProductDto dto)
        {
            var product = new Product()
            {
                ProductId = dto.ProductId,
                Sizes = dto.Sizes,
                Status = dto.Status,
                Colours = dto.Colours,
                Description = dto.Description,
                Price = dto.Price,
            };
            return Ok(_productService.AddProduct(product));
        }

        [HttpDelete]
        public IActionResult DeleteProductById(int id)
        {
            _productService.DeleteProduct(id);
            return Ok();
        }

    }
}