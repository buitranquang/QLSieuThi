using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySieuThi.DTO;
using QuanLySieuThi.BUS;
using QuanLySieuThi.Filter;

namespace QuanLySieuThi.Areas.Admin.Controllers
{
    
    public class ChartProductController : Controller
    {

        // GET: Admin/ChartProduct
        public ActionResult Index()
        {
            CategoryBUS categoryBUS = new CategoryBUS();
            ViewBag.Categories = categoryBUS.GetCategories();

            BillBUS billBUS = new BillBUS();
            List<Bill> bills = billBUS.GetAll();


            List<ProductQuantitySoldModel> productQuantitiesSold = bills.SelectMany(b => 
            b.BillDetails)

               .GroupBy(bd => bd.ProductID)
                                   .Select(g => new ProductQuantitySoldModel
                                   {
                                       ProductId = g.Key,
                                       ProductName = g.First().Product.Name,
                                       ProductPrice = g.First().Product.UnitPrice,
                                       QuantitySold = (int)g.Sum(bd => bd.Quantity)
                                   }).ToList();

            ViewBag.productQuantitiesSold = productQuantitiesSold;
            return View();
        }
    }
}