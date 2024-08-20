
using EShop.Models.Models;

namespace EShop.Models.ViewModels;

public class OrderVM
{
    public OrderHeader OrderHeader { get; set; }
    public IEnumerable<OrderDetail> OrderDetails { get; set; }
}
