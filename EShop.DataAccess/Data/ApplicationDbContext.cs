using EShop.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.DataAccess.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Designers", DisplayOrder = 1 },
            new Category { Id = 2, Name = "Clothing", DisplayOrder = 2 },
            new Category { Id = 3, Name = "Shoes", DisplayOrder = 3 }
            );

    }
}
