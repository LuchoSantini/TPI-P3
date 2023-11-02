using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace TPI_P3.Data.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; } = true;

        public ICollection<Size> Sizes { get; set; } = new List<Size>();
        public ICollection<Colour> Colours { get; set; } = new List<Colour>();
        

    }
}
