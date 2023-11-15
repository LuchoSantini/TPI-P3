namespace TPI_P3.Data.Models
{
    public class OrderLineDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int ColourId { get; set; }
        public int SizeId { get; set; }

    }
}
