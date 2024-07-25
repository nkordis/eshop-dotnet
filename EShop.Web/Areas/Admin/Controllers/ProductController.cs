using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;
using EShop.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController(IWebHostEnvironment webHostEnviroment, IUnitOfWork unitOfWork) : Controller
{
    public IActionResult Index()
    {
        List<Product> products = [.. unitOfWork.Product.GetAll(includeProperties: "Category")];
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
    public IActionResult Upsert(ProductVM productVM, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = webHostEnviroment.WebRootPath;
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                productVM.Product.ImageUrl = @"\images\product\" + filename;
            }
            else
            {
                // If no new file is uploaded and we are updating an existing product
                if (productVM.Product.Id > 0)
                {
                    // Retrieve the existing product to retain the current ImageUrl
                    var existingProduct = unitOfWork.Product.Get(p => p.Id == productVM.Product.Id);
                    if (existingProduct != null)
                    {
                        productVM.Product.ImageUrl = existingProduct.ImageUrl;
                    }
                }
            }


            if (productVM.Product.Id > 0)
            {
                unitOfWork.Product.Update(productVM.Product);
            }
            else
            {
                unitOfWork.Product.Add(productVM.Product);
            }

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

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> objProductList = [.. unitOfWork.Product.GetAll(includeProperties: "Category")];
        return Json(new { data = objProductList });
    }
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        Product? product = unitOfWork.Product.Get(c => c.Id == id);
        if (product == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
        var oldImagePath = Path.Combine(webHostEnviroment.WebRootPath, product.ImageUrl.TrimStart('\\'));
        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }

        unitOfWork.Product.Remove(product);
        unitOfWork.Save();

        return Json(new { success = true, message = "Delete Successfull" });
    }

    #endregion
}

