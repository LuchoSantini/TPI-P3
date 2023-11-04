﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TPI_P3.Data.Entities;
using System.Text.Json.Serialization;

namespace TPI_P3.Data.Entities
{
    public class Colour
    {
        [JsonIgnore]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ColourName { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; } = new List<Product>();


    }

}