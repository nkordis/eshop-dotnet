using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace EShop.Web.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork) : Controller
{
    public IActionResult Index()
    {
        List<Product> products = [.. unitOfWork.Product.GetAll(includeProperties: "Category")];
        return View(products);
    }

    public IActionResult Details(int productid)
    {
        ShoppingCart cart = new()
        {
            Product = unitOfWork.Product.Get(p => p.Id == productid, includeProperties: "Category"),
            ProductId = productid
        };
        
        return View(cart);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Details(ShoppingCart shoppingCart)
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        shoppingCart.ApplicationUserId = userId;

        //check if shopping cart already exists
        ShoppingCart cartFromDb = unitOfWork.ShoppingCart.Get(s => 
            s.ApplicationUserId == userId && s.ProductId == shoppingCart.ProductId);
        
        if (cartFromDb == null)
        {
            unitOfWork.ShoppingCart.Add(shoppingCart);
            unitOfWork.Save();
        }

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

