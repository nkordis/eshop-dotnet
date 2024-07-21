using EShop.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.DataAccess.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Designers", DisplayOrder = 1 },
            new Category { Id = 2, Name = "Clothing", DisplayOrder = 2 },
            new Category { Id = 3, Name = "Shoes", DisplayOrder = 3 }
            );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Title = "Vintage Denim Jacket", Description = "A classic denim jacket in excellent condition.", Size = "M", ListPrice = 15000M, CategoryId = 1, ImageUrl = "" },
            new Product { Id = 2, Title = "Leather Boots", Description = "Sturdy leather boots with minimal wear.", Size = "10", ListPrice = 20000M, CategoryId = 3, ImageUrl = "" },
            new Product { Id = 3, Title = "Summer Dress", Description = "Light and airy dress perfect for summer.", Size = "L", ListPrice = 12000M, CategoryId = 1, ImageUrl = "" },
            new Product { Id = 4, Title = "Wool Sweater", Description = "Cozy wool sweater to keep you warm.", Size = "S", ListPrice = 18000M, CategoryId = 2, ImageUrl = "" },
            new Product { Id = 5, Title = "Formal Shirt", Description = "Elegant shirt for formal occasions.", Size = "M", ListPrice = 10000M, CategoryId = 2, ImageUrl = "" },
            new Product { Id = 6, Title = "Sports Jacket", Description = "Comfortable jacket for sports and outdoor activities.", Size = "L", ListPrice = 25000M, CategoryId = 1, ImageUrl = "" }
            );

    }
}
