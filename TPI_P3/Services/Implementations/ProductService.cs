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
                .AsNoTracking() // estos dos metodos permiten ocultar el array de products ya que no es necesario mostrarlo
                .AsQueryable()
                .Include(p => p.Colours) // incluimos que hayan colores y talles respectivos en cada producto
                .Include(s => s.Sizes)
                .ToList();

        }
    }
}
