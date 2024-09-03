using EShop.DataAccess.Data;
using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;
using EShop.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class UserController(ApplicationDbContext db) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<ApplicationUser> objUserList = [.. db.ApplicationUsers.Include(u => u.Company)];

        foreach (var user in objUserList)
        {
            if (user.Company == null)
            {
                user.Company = new() { Name = "" };
            }
        }

        return Json(new { data = objUserList });
    }
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        return Json(new { success = true, message = "Delete Successfull" });
    }

    #endregion
}

