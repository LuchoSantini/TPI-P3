﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPI_P3.Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }

        [Required]
        public string? UserName { get; set; }
        public string? UserType { get; set; } = "Guest";
        public bool Status { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
