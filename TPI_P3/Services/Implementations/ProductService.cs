using Microsoft.EntityFrameworkCore;
using TPI_P3.Data;
using TPI_P3.Data.Entities;
using TPI_P3.Services.Interfaces;
using System.Drawing;
using TPI_P3.Data.Models;

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

        public Product AddProduct(ProductDto productDto)
        {
            var product = new Product
            {
                Description = productDto.Description,
                Status = productDto.Status,
                Price = productDto.Price
            };

            foreach (var colourId in productDto.ColourId)
            {
                var existingColour = _context.Colours.FirstOrDefault(c => c.Id == colourId);
                if (existingColour == null)
                {
                    throw new ArgumentException($"El Color con el  ID: {colourId} no existe");
                }
                product.Colours.Add(existingColour);
            }

            foreach (var sizeId in productDto.SizeId)
            {
                var existingSize = _context.Sizes.FirstOrDefault(s => s.Id == sizeId);
                if (existingSize == null)
                {
                    throw new ArgumentException($"El talle con el ID: {sizeId} no existe");
                }
                product.Sizes.Add(existingSize);
            }

            //var existingDescription = _context.Products.FirstOrDefault(p => p.Description == productDto.Description);

            //if (productDto.Description != existingDescription.Description)
            //{
            _context.Products.Add(product);
            //}else
            //throw new ArgumentException($"El producto que quieres añadir tiene la misma descripción");
            _context.SaveChanges();

            return product;
        }

        public void DeleteProduct(int productId)
        {
            Product productToBeRemoved = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            productToBeRemoved.Status = false;
            _context.Update(productToBeRemoved);
            _context.SaveChanges();
        }

        public void UpdateProductStatusById(int id)
        {
            Product productToBeEnabled = _context.Products.FirstOrDefault(p => p.ProductId == id);
            productToBeEnabled.Status = true;
            _context.Update(productToBeEnabled);
            _context.SaveChanges();
        }

        public void EditProductById(int id)
        {
            Product productToEdit = _context.Products.FirstOrDefault(p => p.ProductId == id);
            _context.Update(productToEdit);
            _context.SaveChanges();
        }
    }
}
