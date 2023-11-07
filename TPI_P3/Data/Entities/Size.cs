using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TPI_P3.Data.Entities;
using System.Text.Json.Serialization;

public class Size
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SizeId { get; set; }
    public string SizeName { get; set; }
    
}
