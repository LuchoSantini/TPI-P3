using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TPI_P3.Data.Entities;
using System.Text.Json.Serialization;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Description    { get; set; }
    public bool Status { get; set; }
    public decimal Price { get; set; }

    public ICollection<Colour> Colours { get; set; } = new List<Colour>();
    public ICollection<Size> Sizes { get; set; } = new List<Size>();    
}
