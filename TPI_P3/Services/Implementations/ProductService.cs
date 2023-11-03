using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            return _context.Products.ToList();
        }
    }
}
