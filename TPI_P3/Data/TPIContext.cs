using Microsoft.EntityFrameworkCore;
using TPI_P3.Data.Entities;

namespace TPI_P3.Data
{
    public class TPIContext : DbContext
    {
       // public DbSet<Order> Orders { get; set; } //lo que hagamos con LINQ sobre estos DbSets lo va a transformar en consultas SQL
        public DbSet<Product> Products { get; set; } //Los warnings los podemos obviar porque DbContext se encarga de eso.
        //public DbSet<User> Users { get; set; }
        public DbSet<Variant> Variants { get; set; }

        public TPIContext(DbContextOptions<TPIContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasDiscriminator(u => u.UserType);

            modelBuilder.Entity<Product>()
                .ToTable("ProductTable") 
                .HasData(
                    new Product
                    {
                        Id = 1,
                        Description = "Zapatilla Nike",
                        Price = 1700,
                        Status = true
                    },
                    new Product
                    {
                        Id = 2,
                        Description = "Zapatilla Adidas",
                        Price = 1600,
                        Status = true
                    });


            modelBuilder.Entity<Variant>().HasData(
                new Variant
                {
                    Id = 1,
                    Status = false,
                    Colour = "AZUL",
                    Size = "L",
                },
                new Variant
                {
                    Id = 2,
                    Status = false,
                    Colour = "rojo",
                    Size = "L",
                });

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Variants)
                .WithOne(v => v.Product)
                .HasForeignKey(v => v.ProductId);

            base.OnModelCreating(modelBuilder);
        }



    }
    }

