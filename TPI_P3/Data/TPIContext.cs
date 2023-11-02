using Microsoft.EntityFrameworkCore;
using TPI_P3.Data.Entities;

namespace TPI_P3.Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Order> Orders { get; set; } 
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<Size>  Sizes { get; set; }


        public TPIContext(DbContextOptions<TPIContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasDiscriminator(u => u.UserType);


            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "Seba",
                    Password = "123456",
                    Status = true,
                    UserName = "SebaR",
                    UserType = "Admin",
                }
                );

            modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        Id = 1,
                        Description = "Zapatilla Nike",
                        Price = 1700,
                        Status = true,



                    },
                    new Product
                    {
                        Id = 2,
                        Description = "Zapatilla Adidas",
                        Price = 1600,
                        Status = true,


                    });



            modelBuilder.Entity<Colour>().HasData(
                new Colour
                {
                    Id = 1,
                    ColourName = "Azul"
                },
                new Colour
                {
                    Id = 2,
                    ColourName = "Rojo"
                });

            modelBuilder.Entity<Size>().HasData(
                new Size
                {
                    Id = 4,
                    SizeName = "L",
                },
                new Size
                {
                    Id = 6,
                    SizeName = "XXL",
                }
                );



            // TABLA ENTRE PRODUCT Y SIZE
            modelBuilder.Entity<Product>()
             .HasMany(s => s.Sizes)
             .WithMany(c => c.Products)
             .UsingEntity(j => j
                .ToTable("SizesProducts")
                .HasData(new[]
                {
                    new{SizesId=4, ProductsId=1},
                    new{SizesId=6, ProductsId=2}
                }
                )
                );

            // TABLA ENTRE PRODUCT Y COLOUR
            modelBuilder.Entity<Product>()
                .HasMany(c => c.Colours)
                .WithMany(p => p.Products)
                .UsingEntity(j => j
                    .ToTable("ColoursProducts")
                    .HasData(new[]
                    {
                        new{ColoursId=1, ProductsId=2}
                    }
                    ));



        }
    }
}

