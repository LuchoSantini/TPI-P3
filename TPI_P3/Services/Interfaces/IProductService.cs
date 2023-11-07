namespace TPI_P3.Services.Interfaces
{
    public interface IProductService
    {
        public List<Product> GetProducts();
        public Product GetProductById(int id);
        public Product AddProduct(Product product);
        public void DeleteProduct(int productId);
    }
}
