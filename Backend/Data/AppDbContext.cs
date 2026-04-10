using Microsoft.EntityFrameworkCore;
using TechStoreInventory.Models;

namespace TechStoreInventory.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Seed dữ liệu mẫu
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Laptop" },
            new Category { Id = 2, Name = "Điện thoại" },
            new Category { Id = 3, Name = "Phụ kiện" }
        );
        
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Dell XPS 13", Price = 1200, Quantity = 5, CategoryId = 1 },
            new Product { Id = 2, Name = "iPhone 15", Price = 999, Quantity = 10, CategoryId = 2 },
            new Product { Id = 3, Name = "Tai nghe Airpods", Price = 249, Quantity = 20, CategoryId = 3 }
        );
    }
}