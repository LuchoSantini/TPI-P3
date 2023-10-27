using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPI_P3.Data.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }

        public ICollection<Variant> Variants { get; set; } = new List<Variant>();
        // creamos una list de variantes en el product porque queremos relacionar productos
        // y variantes donde por ej: tenemos remera 1 y remera 1 tiene color blanco,negro y rojo

    }
}
