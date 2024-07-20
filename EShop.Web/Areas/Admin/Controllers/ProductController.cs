using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController(IUnitOfWork unitOfWork) : Controller
{
    public IActionResult Index()
    {
        List<Product> products = [.. unitOfWork.Product.GetAll()];

        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            unitOfWork.Product.Add(product);
            unitOfWork.Save();
            TempData["success"] = "Product created successfully";
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

        Product? product = unitOfWork.Product.Get(c => c.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            unitOfWork.Product.Update(product);
            unitOfWork.Save();
            TempData["success"] = "Product updated successfully";
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

        Product? product = unitOfWork.Product.Get(c => c.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Product? product = unitOfWork.Product.Get(c => c.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        unitOfWork.Product.Remove(product);
        unitOfWork.Save();
        TempData["success"] = "Product deleted successfully";
        return RedirectToAction("Index");
    }
}

