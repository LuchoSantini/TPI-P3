using TPI_P3.Data.Entities;

namespace TPI_P3.Data.Models
{
    public class ProductToEditDto
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<int> ColourIds { get; set; } = new List<int>();
        public ICollection<int> SizeIds { get; set; } = new List<int>();
    }
}
