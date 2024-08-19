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

    public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
    {
        var orderFromDb = db.OrderHeaders.First(x => x.Id == id);
        if (orderFromDb != null)
        {
            orderFromDb.OrderStatus = orderStatus;
            if (!string.IsNullOrEmpty(paymentStatus))
            {
                orderFromDb.PaymentStatus = paymentStatus;
            }
        }
    }

    public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
    {
        var orderFromDb = db.OrderHeaders.First(x => x.Id == id);
        if (!string.IsNullOrEmpty(sessionId))
        {
            orderFromDb.SessionId = sessionId;
        }
        if (!string.IsNullOrEmpty(paymentIntentId))
        {
            orderFromDb.PaymentIntentId = paymentIntentId;
            orderFromDb.PaymentDate = DateTime.Now;
        }
    }
}

