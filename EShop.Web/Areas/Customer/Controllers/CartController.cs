using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;
using EShop.Models.ViewModels;
using EShop.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.Web.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize]
public class CartController(IUnitOfWork unitOfWork) : Controller
{
    [BindProperty]
    public ShoppingCartVM ShoppingCartVM { get; set; }
    public IActionResult Index()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCartVM = new()
        {
            ShoppingCarts = unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == userId, includeProperties: "Product"),
            OrderHeader = new()
        };

        ShoppingCartVM.OrderHeader.OrderTotal = ShoppingCartVM.ShoppingCarts.Sum(s => s.Product.ListPrice);

        return View(ShoppingCartVM);
    }

    public IActionResult Summary()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCartVM = new()
        {
            ShoppingCarts = unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == userId, includeProperties: "Product"),
            OrderHeader = new()
        };

        ShoppingCartVM.OrderHeader.ApplicationUser = unitOfWork.ApplicationUser.Get(u => u.Id == userId);
        ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
        ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
        ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
        ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
        ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
        ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

        ShoppingCartVM.OrderHeader.OrderTotal = ShoppingCartVM.ShoppingCarts.Sum(s => s.Product.ListPrice);

        return View(ShoppingCartVM);
    }

    [HttpPost]
    [ActionName("Summary")]
    public IActionResult SummaryPost()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;


        ShoppingCartVM.ShoppingCarts = unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,
            includeProperties: "Product");

        ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
        ShoppingCartVM.OrderHeader.ApplicationUserId = userId;
        ApplicationUser applicationUser = unitOfWork.ApplicationUser.Get(u => u.Id == userId);

        ShoppingCartVM.OrderHeader.OrderTotal = ShoppingCartVM.ShoppingCarts.Sum(s => s.Product.ListPrice);

        if(applicationUser.CompanyId.GetValueOrDefault() == 0)
        {
            ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
        }
        else
        {
            ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
            ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusApproved;
        }
        unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
        unitOfWork.Save();

        foreach (var cart in ShoppingCartVM.ShoppingCarts)
        {
            OrderDetail orderDetail = new()
            {
                ProductId = cart.ProductId,
                OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                Price = cart.Product.ListPrice,
            };
            unitOfWork.OrderDetail.Add(orderDetail);
            unitOfWork.Save();
        }

        if (applicationUser.CompanyId.GetValueOrDefault() == 0)
        {
            // regular customer account and we need to capture payment
            // stripe payment
        }

        //return View(ShoppingCartVM);
        return RedirectToAction(nameof(OrderConfirmation), new {id=ShoppingCartVM.OrderHeader.Id});
    }

    public IActionResult OrderConfirmation(int id)
    {
        return View(id);
    }

    public IActionResult Remove(int cartId)
    {
        var cartFromDb = unitOfWork.ShoppingCart.Get(c => c.Id == cartId);
        unitOfWork.ShoppingCart.Remove(cartFromDb);
        unitOfWork.Save();

        return RedirectToAction(nameof(Index)); ;
    }
}
