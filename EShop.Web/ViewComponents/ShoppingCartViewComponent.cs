using EShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.Web.ViewComponents;

public class ShoppingCartViewComponent(IUnitOfWork unitOfWork) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var cartCount = userId == null ? 0 : unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == userId).Count();

        return View(cartCount);
    }
}
