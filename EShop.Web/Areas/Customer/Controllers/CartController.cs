using EShop.DataAccess.Repository.IRepository;
using EShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.Web.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize]
public class CartController(IUnitOfWork unitOfWork) : Controller
{
    public ShoppingCartVM ShoppingCartVM { get; set; }
    public IActionResult Index()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCartVM = new()
        {
            ShoppingCarts = unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == userId, includeProperties: "Product")
        };

        ShoppingCartVM.OrderTotal = ShoppingCartVM.ShoppingCarts.Sum(s => s.Product.ListPrice);

        return View(ShoppingCartVM);
    }
}
