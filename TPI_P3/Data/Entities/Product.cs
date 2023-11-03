using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TPI_P3.Data.Entities;
using System.Text.Json.Serialization;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; }
    public decimal Price { get; set; }

    public ICollection<ProductColour> ProductColours { get; set; } // Tabla intermedia ProductColour
    public ICollection<ProductSize> ProductSizes { get; set; } // Tabla intermedia ProductSize
}
