using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers;
public class CategoryController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

