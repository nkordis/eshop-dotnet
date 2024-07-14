using EShop.Web.Data;
using EShop.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers;
public class CategoryController(ApplicationDbContext _applicationDbContext) : Controller
{
    public IActionResult Index()
    {
        List<Category> categories = [.. _applicationDbContext.Categories];

        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _applicationDbContext.Categories.Add(category);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        return View();
    }
}

