using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;
using EShop.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CompanyController(IUnitOfWork unitOfWork) : Controller
{
    public IActionResult Index()
    {
        List<Company> Companys = [.. unitOfWork.Company.GetAll()];
        return View(Companys);
    }
    public IActionResult Upsert(int? id)
    {
        Company company = id > 0 ? unitOfWork.Company.Get(p => p.Id == id) : new Company();
        
        return View(company);
    }
    [HttpPost]
    public IActionResult Upsert(Company company)
    {
        if (ModelState.IsValid)
        {
            if (company.Id > 0)
            {
                unitOfWork.Company.Update(company);
            }
            else
            {
                unitOfWork.Company.Add(company);
            }

            unitOfWork.Save();
            TempData["success"] = "Company created successfully";
            return RedirectToAction("Index");
        }

            return View(company);
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Company> objCompanyList = [.. unitOfWork.Company.GetAll()];
        return Json(new { data = objCompanyList });
    }
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        Company? Company = unitOfWork.Company.Get(c => c.Id == id);
        if (Company == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        unitOfWork.Company.Remove(Company);
        unitOfWork.Save();

        return Json(new { success = true, message = "Delete Successfull" });
    }

    #endregion
}

