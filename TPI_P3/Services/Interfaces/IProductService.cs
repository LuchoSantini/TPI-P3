using TPI_P3.Data.Dto;

namespace TPI_P3.Services.Interfaces
{
    public interface IProductService
    {
        public List<Product> GetProducts();
        public Product GetProductById(int id);
        public Product AddProduct(ProductDto dto);
        public void DeleteProduct(int productId);
        public void UpdateProductStatusById(int id);
        public void EditProductById(int id);
    }
}
