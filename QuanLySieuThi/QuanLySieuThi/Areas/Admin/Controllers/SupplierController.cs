using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySieuThi.BUS;
using QuanLySieuThi.DTO;
namespace QuanLySieuThi.Areas.Admin.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Admin/Supplier
        public ActionResult Index()
        {
            SupplierBUS supplierBUS = new SupplierBUS();
            ViewBag.Suppliers=supplierBUS.GetSuppliers();
            return View();
        }

        public ActionResult Edit(int id)
        {
            SupplierBUS supplierBUS = new SupplierBUS();
            Supplier supp = supplierBUS.GetSupplierById(id);
            ViewBag.Supplier = supp;
            return View(supp);
        }

        [HttpPost]
        public ActionResult EditSupplier()
        {
            string ID = Request.Form["ID"];
            string Name = Request.Form["Name"];
            string Description = Request.Form["Description"];
            SupplierBUS supplierBUS = new SupplierBUS();
            Supplier supp = new Supplier();
            supp = supplierBUS.GetSupplierById(Int32.Parse(ID));
            if(supplierBUS.UpdateInfo(supp, Name, Description))
                TempData["SuccessMsg"] = "Cập nhật thành công";
            return RedirectToAction("Index", "Supplier");
        }
        public ActionResult Delete(int id)
        {
            SupplierBUS supplierBUS = new SupplierBUS();
            supplierBUS.Delete(id);
            return RedirectToAction("Index", "Supplier");
        }


        [HttpPost]
        public ActionResult AddSupplier()
        {
            string Name = Request.Form["Name"];
            string Description = Request.Form["Description"];
            SupplierBUS supplierBUS = new SupplierBUS();
            Supplier supp = new Supplier(Name, Description);
            supplierBUS.Create(supp);
            return RedirectToAction("Index", "Supplier");
        }
    }
}
