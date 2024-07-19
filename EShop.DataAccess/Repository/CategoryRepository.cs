using EShop.DataAccess.Data;
using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;

namespace EShop.DataAccess.Repository;

public class CategoryRepository(ApplicationDbContext db) 
    : Repository<Category>(db), ICategoryRepository
{
    public void Update(Category category)
    {
        db.Update(category);
    }
}

