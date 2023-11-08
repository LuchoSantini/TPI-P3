using TPI_P3.Data.Entities;

namespace TPI_P3.Data.Dto
{
    public class ProductDto
    {
        //public int ProductId { get; set; } //Se autogenera
        public string? Description { get; set; }
        public bool Status { get; set; } = true;
        public decimal Price { get; set; }
        public List<int> ColourId { get; set; }
        public List<int> SizeId { get; set; }
    }
}
