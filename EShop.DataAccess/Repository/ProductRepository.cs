using EShop.DataAccess.Data;
using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;

namespace EShop.DataAccess.Repository;

public class ProductRepository(ApplicationDbContext db) 
    : Repository<Product>(db), IProductRepository
{
    public void Update(Product product)
    {
        db.Update(product);
    }
}
