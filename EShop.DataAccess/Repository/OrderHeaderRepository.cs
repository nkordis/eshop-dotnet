using EShop.DataAccess.Data;
using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;

namespace EShop.DataAccess.Repository;

public class OrderHeaderRepository(ApplicationDbContext db) 
    : Repository<OrderHeader>(db), IOrderHeaderRepository
{
    public void Update(OrderHeader obj)
    {
        db.Update(obj);
    }
}

