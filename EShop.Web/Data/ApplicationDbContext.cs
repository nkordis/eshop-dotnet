using EShop.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.Web.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
}
