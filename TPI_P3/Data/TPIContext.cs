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
        public DbSet<Size> Sizes { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }


        public TPIContext(DbContextOptions<TPIContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasDiscriminator(u => u.UserType);


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
                    Id = 1,
                    SizeName = "L",
                },
                new Size
                {
                    Id = 2,
                    SizeName = "XL",
                }
                );

            
                


            // TABLA ENTRE PRODUCT Y SIZE
            modelBuilder.Entity<Product>()
             .HasMany(p => p.Sizes)
             .WithMany()
             .UsingEntity(j => j
                .ToTable("SizesProducts")
             );

            // TABLA ENTRE PRODUCT Y COLOUR
            modelBuilder.Entity<Product>()
            .HasMany(p => p.Colours)
            .WithMany()
            .UsingEntity(j => j
                .ToTable("ColoursProducts")
            );

            modelBuilder.Entity<OrderLine>()
            .HasOne(ol => ol.Product)
            .WithMany()
            .HasForeignKey(ol => ol.ProductId);



            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderLines)
                .WithOne()
                .HasForeignKey(ol => ol.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);


        }
    }
}

