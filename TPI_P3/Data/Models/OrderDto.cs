using System.ComponentModel.DataAnnotations.Schema;
using TPI_P3.Data.Entities;

namespace TPI_P3.Data.Models
{
    public class OrderDto
    {
        public bool Status { get; set; } = true;
        public int UserId { get; set; }
    }
}
