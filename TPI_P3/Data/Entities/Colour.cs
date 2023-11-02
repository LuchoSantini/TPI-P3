using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TPI_P3.Data.Entities;

namespace TPI_P3.Data.Entities
{
    public class Colour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ColourName { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; } = new List<Product>();


    }

}