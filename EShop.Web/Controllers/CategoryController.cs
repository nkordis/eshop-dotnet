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

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? category = _applicationDbContext.Categories.Find(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }
    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _applicationDbContext.Categories.Update(category);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? category = _applicationDbContext.Categories.Find(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? category = _applicationDbContext.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }
        _applicationDbContext.Remove(category);
        _applicationDbContext.SaveChanges();
        return RedirectToAction("Index");
    }
}

