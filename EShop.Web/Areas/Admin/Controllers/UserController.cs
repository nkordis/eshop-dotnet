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

        var userRoles = db.UserRoles.ToList();
        var roles = db.Roles.ToList();

        foreach (var user in objUserList)
        {
            var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
            user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;

            if (user.Company == null)
            {
                user.Company = new() { Name = "" };
            }
        }

        return Json(new { data = objUserList });
    }
    [HttpPost]
    public IActionResult LockUnlock([FromBody] string id)
    {
        var objFromDb = db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
        if (objFromDb == null)
        {
            return Json(new { success = false, message = "Error while Locking/Unlocking" });
        }

        if(objFromDb.LockoutEnd!=null && objFromDb.LockoutEnd > DateTime.Now)
        {
            // user is currently locked and we need to unlock them
            objFromDb.LockoutEnd = DateTime.Now;
        }
        else
        {
            objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
        }
        db.SaveChanges();

        return Json(new { success = true, message = "Operation Successfull" });
    }

    #endregion
}

