using Microsoft.EntityFrameworkCore;
using TPI_P3.Data.Entities;

namespace TPI_P3.Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Order> Orders { get; set; } // Agregado DbSet para Order
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; } // Agregado DbSet para User
        public DbSet<Size> Sizes { get; set; } // Agregado DbSet para Size
        public DbSet<Colour> Colours { get; set; } // Agregado DbSet para Colour

        public TPIContext(DbContextOptions<TPIContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);

            modelBuilder.Entity<Size>()
                .HasKey(s => s.SizeId);

            modelBuilder.Entity<Size>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Sizes)
                .HasForeignKey(s => s.ProductId);

            modelBuilder.Entity<Colour>()
                .HasKey(c => c.ColourId);

            modelBuilder.Entity<Colour>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Colours)
                .HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
        }
    }
}
