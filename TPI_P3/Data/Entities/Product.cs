using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace TPI_P3.Data.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public decimal Price { get; set; }
        public ICollection<Size> Sizes { get; set; }
        public ICollection<Colour> Colours { get; set; }


    }
}
