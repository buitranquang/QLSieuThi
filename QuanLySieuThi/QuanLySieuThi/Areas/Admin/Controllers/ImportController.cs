using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySieuThi.DTO;
using QuanLySieuThi.BUS;
using System.Linq;
namespace QuanLySieuThi.Areas.Admin.Controllers
{
    public class ImportController : Controller
    {
        private List<ImportBillDetail> ListImportBillDetail
        {
            get
            {
                var importDetails = Session["ListImportBillDetail"] as List<ImportBillDetail>;
                if (importDetails == null)
                {
                    importDetails = new List<ImportBillDetail>();
                    Session["ListImportBillDetail"] = importDetails;
                }
                return importDetails;
            }
            set
            {
                Session["ListImportBillDetail"] = value;
            }
        }
        // GET: Admin/Import
        public ActionResult Index()
        {
            ProductBUS productBUS = new ProductBUS();
            List<Product> products = productBUS.GetProducts();
            ViewBag.Products = products;
            return View();
        }

        public ActionResult Add()
        {
            ProductBUS productBUS = new ProductBUS();
            List<Product> products = productBUS.GetProducts();
            ViewBag.Products = products;
            return View();
        }
        [HttpPost]
        public ActionResult ImportProduct()
        {
            ProductBUS productBUS = new ProductBUS();
            string productID = Request.Form["ID"];
            int importnumber = int.Parse(Request.Form["ImportNumber"]);
            decimal UnitPrice = decimal.Parse(Request.Form["UnitPrice"]);
            Product p = productBUS.GetProduct(productID);

            var listDetail = this.ListImportBillDetail;
            if (listDetail.Exists(item => item.ProductID == p.ID))
            {
                return Json(new { fail = "Không thể thêm sản phẩm đã có rồi" });
            }
            else
            {
                ImportBillDetail importbilldetail = new ImportBillDetail() { Quantity = importnumber, Product = p, Price= UnitPrice };
                listDetail.Add(importbilldetail);
                this.ListImportBillDetail = listDetail;
                return Json(new { success = "Thêm thành công", importnumber = importbilldetail.Quantity, productName = importbilldetail.Product.Name, unitinstock = p.UnitInStock, pID = productID, price = (importbilldetail.Price* importnumber) });
            }

        }

        public ActionResult Save()
        {
            DateTime currentDateTime = DateTime.Now;
            ImportBillBUS importBillBUS = new ImportBillBUS();
            ImportBill importBill = new ImportBill() { CreatedDate = currentDateTime };
            if (importBillBUS.Add(importBill, this.ListImportBillDetail) != 0)
            { TempData["SuccessMsg"] = "Khởi tạo sự kiện thành công"; }
            return RedirectToAction("Index", "Import");

        }
        [HttpPost]
        public ActionResult DeleteDetail(int productID)
        {
            //string productID = Request.Form["productID"];
            var listDetail = this.ListImportBillDetail;
            ImportBillDetail ed = listDetail.FirstOrDefault(e => e.Product.ID == productID);
            if (ed != null)
            {
                listDetail.Remove(ed);
                this.ListImportBillDetail = listDetail;
            }
            return Json(new { pID = productID });
        }

        [HttpPost]
        public ActionResult Delete(int evtID)
        {
            ImportBillBUS importBillBUS = new ImportBillBUS();
            if (importBillBUS.Delete(evtID) > 0)
            {
                TempData["SuccessMsg"] = "Xóa thành công";
            }
            else
                TempData["FailMsg"] = "Không thành công";
            return Json(new { redirectToUrl = Url.Action("Index", "Event") });
        }
    }
}