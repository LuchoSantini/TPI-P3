using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TPI_P3.Data.Entities;

public class Colour
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ColourId { get; set; }
    public string Name { get; set; }
    public ICollection<ProductColour> ProductColours { get; set; } // Tabla intermedia ProductColour
}
