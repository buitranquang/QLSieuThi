using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySieuThi.BUS;
using QuanLySieuThi.DTO;
namespace QuanLySieuThi.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Admin/Customer
        public ActionResult Index()
        {
            CustomerBUS customerBUS = new CustomerBUS();
            ViewBag.Customers = customerBUS.GetCustomers();
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int customerId)
        {
            CustomerBUS customerBUS = new CustomerBUS();
            if (customerBUS.Delete(customerId) > 0)
            {
                TempData["SuccessMsg"] = "Xóa khách hàng thành công!!!";
            }
            return Json(new { redirectToUrl = Url.Action("Index", "Customer") });
        }
    }
}