using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TPI_P3.Data.Entities;
using System.Text.Json.Serialization;

namespace TPI_P3.Data.Entities
{
    public class Colour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColourId { get; set; }
        public string ColourName { get; set; } = string.Empty;
        


    }

}