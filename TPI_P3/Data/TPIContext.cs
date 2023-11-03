using Microsoft.EntityFrameworkCore;
using TPI_P3.Data.Entities;

namespace TPI_P3.Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductColour> ProductColours { get; set; }

        public TPIContext(DbContextOptions<TPIContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the relationships between entities
            modelBuilder.Entity<ProductSize>()
                .HasKey(ps => new { ps.ProductId, ps.SizeId });

            modelBuilder.Entity<ProductColour>()
                .HasKey(pc => new { pc.ProductId, pc.ColourId });

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<ProductSize>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.ProductSizes)
                .HasForeignKey(ps => ps.ProductId);

            modelBuilder.Entity<ProductSize>()
                .HasOne(ps => ps.Size)
                .WithMany(s => s.ProductSizes)
                .HasForeignKey(ps => ps.SizeId);

            modelBuilder.Entity<ProductColour>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductColours)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductColour>()
                .HasOne(pc => pc.Colour)
                .WithMany(c => c.ProductColours)
                .HasForeignKey(pc => pc.ColourId);

            // Seed data for entities
            modelBuilder.Entity<Colour>().HasData(
                    new Colour { ColourId = 1, Name = "Red" },
                    new Colour { ColourId = 2, Name = "Blue" }
                );

            modelBuilder.Entity<Size>().HasData(
                new Size { SizeId = 1, Name = "S" },
                new Size { SizeId = 2, Name = "L" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Product 1", Status = true, Price = 1000 }
            );

            modelBuilder.Entity<ProductColour>().HasData(
                new ProductColour { ProductId = 1, ColourId = 1 },
                new ProductColour { ProductId = 1, ColourId = 2 }
            );

            modelBuilder.Entity<ProductSize>().HasData(
                new ProductSize { ProductId = 1, SizeId = 1 },
                new ProductSize { ProductId = 1, SizeId = 2 }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, UserName = "exampleUser1", Status = true },
                new User { UserId = 2, UserName = "exampleUser2", Status = true }
            );//completar bien la data del User

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, Status = true, UserId = 1, ProductId = 1 },
                new Order { Id = 2, Status = true, UserId = 2, ProductId = 1 }
            );
        }
    }
}
