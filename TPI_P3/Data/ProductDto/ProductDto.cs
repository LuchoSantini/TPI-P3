using TPI_P3.Data.Entities;

namespace TPI_P3.Data.ProductDto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public decimal Price { get; set; }
        public ICollection<Colour> Colours { get; set; } = new List<Colour>();
        public ICollection<Size> Sizes { get; set; } = new List<Size>();
    }
}
