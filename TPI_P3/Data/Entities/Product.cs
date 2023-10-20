namespace TPI_P3.Data.Entities
{
    public class Product
    {
        // Preguntar al profe como order y product se relacionan
        public int Id { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? Status { get; set; }
    }
}
