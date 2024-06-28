using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySieuThi.BUS;
using QuanLySieuThi.DTO;
using QuanLySieuThi.Filter;
namespace QuanLySieuThi.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        [CommonAttributeFilter]
        // GET: Admin/Category
        public ActionResult Index()
        {
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            return View();
        }
        public ActionResult Edit(int id)
        {
            CategoryBUS bus = new CategoryBUS();
            Category cate = bus.GetCategoryById(id);
            ViewBag.Cate = cate;
            return View(cate);
        }
        [HttpPost]
        public ActionResult EditCategory()
        {
            string ID = Request.Form["ID"];
            string Name = Request.Form["Name"];
            string Description = Request.Form["Description"];
            Category cate = new Category();
            CategoryBUS categoryBUS = new CategoryBUS();
            cate = categoryBUS.GetCategoryById(Int32.Parse(ID));
            if (categoryBUS.UpdateInfo(cate, Name, Description))
                TempData["SuccessMsg"] = "Cập nhật thành công";

            return RedirectToAction("Index", "Category");
        }
        public ActionResult Delete(int id)
        {
            CategoryBUS categoryBUS = new CategoryBUS();
            categoryBUS.DeleteCategory(id);
            return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        public ActionResult AddCategory()
        {
            string Name = Request.Form["Name"];
            string Description = Request.Form["Description"];
            Category cate = new Category(Name, Description);
            CategoryBUS categoryBUS = new CategoryBUS();
            categoryBUS.AddCategory(cate);
            return RedirectToAction("Index", "Category");
        }
    }
}