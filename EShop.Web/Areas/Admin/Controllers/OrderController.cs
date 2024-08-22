using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;
using EShop.Models.ViewModels;
using EShop.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class OrderController(IUnitOfWork unitOfWork) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Details(int orderId)
    {
        OrderVM orderVM = new()
        {
            OrderHeader = unitOfWork.OrderHeader.Get(u => u.Id == orderId, includeProperties: "ApplicationUser"),
            OrderDetails = unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
        };

        return View(orderVM);
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll(string status)
    {
        IEnumerable<OrderHeader> objOrderHeaders = [.. unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser")];

        switch (status)
        {
            case "pending":
                objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == SD.PaymentStatusDelayedPayment);
                break;
            case "inprocess":
                objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == SD.StatusInProcess);
                break;
            case "completed":
                objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == SD.StatusShipped);
                break;
            case "approved":
                objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == SD.StatusApproved);
                break;
            default:
                break;
        }

        return Json(new { data = objOrderHeaders });
    }

    #endregion
}
