using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TPI_P3.Data.Entities;

public class Size
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SizeId { get; set; }
    public string Name { get; set; }
    public ICollection<ProductSize> ProductSizes { get; set; } // Tabla intermedia ProductSize
}
