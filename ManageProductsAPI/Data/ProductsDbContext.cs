using ManageProductsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageProductsAPI.Data
{
    public class ProductsDbContext:DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the 'Id' property as the primary key and identity column
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);  // Ensure 'Id' is the primary key

            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()  // This makes the column an identity column
                .IsRequired();  // This ensures that 'Id' cannot be null

        }
    }
}
