using Microsoft.EntityFrameworkCore;
using TPI_P3.Data;
using TPI_P3.Data.Entities;
using TPI_P3.Services.Interfaces;

namespace TPI_P3.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly TPIContext _context;

        public ProductService(TPIContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts()
        {
            return _context.Products
                .Include(p => p.Colours) // incluimos que hayan colores y talles respectivos en cada producto
                .Include(p => p.Sizes)
                .ToList();

        }

        public Product? GetProductById(int id)
        {
            return _context.Products
                .Include(p => p.Colours)
                .Include(p => p.Sizes)
                .FirstOrDefault(x => x.ProductId == id);
        }
    }
}
