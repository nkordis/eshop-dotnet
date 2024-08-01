using EShop.DataAccess.Data;
using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;

namespace EShop.DataAccess.Repository;

public class ShoppingCartRepository(ApplicationDbContext db) 
    : Repository<ShoppingCart>(db), IShoppingCartRepository
{
    public void Update(ShoppingCart shoppingCart)
    {
        db.Update(shoppingCart);
    }
}

