using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;
using EShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController(IUnitOfWork unitOfWork) : Controller
{
    public IActionResult Index()
    {
        List<Product> products = [.. unitOfWork.Product.GetAll()];
        return View(products);
    }
    public IActionResult Upsert(int? id)
    {
        IEnumerable<SelectListItem> CategoryList = unitOfWork.Category.GetAll().Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        });

        ProductVM productVM = new()
        {
            CategoryList = CategoryList,
            Product = id > 0 ? unitOfWork.Product.Get(p => p.Id == id) : new Product()
        };

        return View(productVM);
    }
    [HttpPost]
    public IActionResult Upsert(ProductVM productVM, IFormFile? formFile)
    {
        if (ModelState.IsValid)
        {
            unitOfWork.Product.Add(productVM.Product);
            unitOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
        else
        {
            productVM.CategoryList = unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            return View(productVM);
        }
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

