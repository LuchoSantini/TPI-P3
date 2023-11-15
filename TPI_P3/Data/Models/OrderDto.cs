using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TPI_P3.Data.Entities;

namespace TPI_P3.Data.Models
{
    public class OrderDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool Status { get; set; } = true;
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
