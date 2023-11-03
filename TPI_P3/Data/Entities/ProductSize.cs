using System.ComponentModel.DataAnnotations.Schema;

public class ProductSize
{
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    public int ProductId { get; set; }

    [ForeignKey("SizeId")]
    public Size Size { get; set; }
    public int SizeId { get; set; }

}
