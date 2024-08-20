using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;
using EShop.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class OrderController(IUnitOfWork unitOfWork) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<OrderHeader> objOrderHeaders = [.. unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser")];
        return Json(new { data = objOrderHeaders });
    }

    #endregion
}
