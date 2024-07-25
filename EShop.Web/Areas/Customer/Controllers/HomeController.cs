using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EShop.Web.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork) : Controller
{
    public IActionResult Index()
    {
        List<Product> products = [.. unitOfWork.Product.GetAll(includeProperties: "Category")];
        return View(products);
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

