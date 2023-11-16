using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPI_P3.Data.Entities
{
    public class OrderLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public int? ColourId { get; set; }
        [ForeignKey("ColourId")]
        public Colour Colour { get; set; }
        public int? SizeId { get; set; }
        [ForeignKey("SizeId")]
        public Size Size { get; set; }
    }
}
