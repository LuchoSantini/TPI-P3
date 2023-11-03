using System.ComponentModel.DataAnnotations.Schema;

public class ProductColour
{

    [ForeignKey("ProductId")]

    public Product Product { get; set; }
    public int ProductId { get; set; }
    [ForeignKey("ColourId")]

    public Colour Colour { get; set; }
    public int ColourId { get; set; }
}
