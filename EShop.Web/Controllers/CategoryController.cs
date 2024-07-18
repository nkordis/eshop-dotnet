using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers;
public class CategoryController(ICategoryRepository categoryRepository) : Controller
{
    public IActionResult Index()
    {
        List<Category> categories = [.. categoryRepository.GetAll()];

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
            categoryRepository.Add(category);
            categoryRepository.Save();
            TempData["success"] = "Category created successfully";
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

        Category? category = categoryRepository.Get(c => c.Id == id);

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
            categoryRepository.Update(category);
            categoryRepository.Save();
            TempData["success"] = "Category updated successfully";
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

        Category? category = categoryRepository.Get(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? category = categoryRepository.Get(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        categoryRepository.Remove(category);
        categoryRepository.Save();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}

