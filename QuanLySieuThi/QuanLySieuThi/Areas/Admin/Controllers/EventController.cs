using QuanLySieuThi.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySieuThi.DTO;
using System.Diagnostics;

namespace QuanLySieuThi.Areas.Admin.Controllers
{
    public class EventController : Controller
    {
        private List<EventDetail> ListEventDetail
        {
            get
            {
                var eventDetails = Session["ListEventDetail"] as List<EventDetail>;
                if (eventDetails == null)
                {
                    eventDetails = new List<EventDetail>();
                    Session["ListEventDetail"] = eventDetails;
                }
                return eventDetails;
            }
            set
            {
                Session["ListEventDetail"] = value;
            }
        }

        // GET: Admin/Event
        public ActionResult Index()
        {
            EventBUS eventBUS = new EventBUS();
            List<Event> events = eventBUS.GetEventsFromNow();
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            ViewBag.FailMsg = TempData["FailMsg"];
            ViewBag.Events = events;
            return View();
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            EventBUS eventBUS = new EventBUS();
            Event evt = eventBUS.GetEventById(id ?? 0);
            ViewBag.Event = evt;
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            
            return View();
        }
        [HttpPost]
        public ActionResult DeleteEventDetail(int edId, int eventId)
        {
            EventBUS eventBUS = new EventBUS();
            if (eventBUS.RemoveDetail(edId) > 0)
                TempData["SuccessMsg"] = "Xóa thành công!!";
            Event evt = eventBUS.GetEventById(eventId);
            if (evt != null)
                return Json(new { redirectToUrl = Url.Action("Detail", "Event", new { id = eventId }) });
            else
                return Json(new { redirectToUrl = Url.Action("Index", "Event") });
        }
        public ActionResult Add()
        {
            ProductBUS productBUS = new ProductBUS();
            List<Product> products = productBUS.GetProducts();
            ViewBag.Products = products;
            return View();
        }

        [HttpPost]
        public ActionResult AddDetail()
        {
            ProductBUS productBUS = new ProductBUS();
            string productID = Request.Form["ID"];
            Decimal discount = Decimal.Parse(Request.Form["discount"]);
            Product p = productBUS.GetProduct(productID);

            var listDetail = this.ListEventDetail;
            if (listDetail.Exists(item => item.ProductID == p.ID))
            {
                return Json(new { fail = "Không thể thêm sản phẩm đã có rồi" });
            }
            else
            {
                EventDetail eventDetail = new EventDetail() { DiscountPrice = discount, Product = p };
                listDetail.Add(eventDetail);
                this.ListEventDetail = listDetail;
                return Json(new { success = "Thêm thành công", discount = eventDetail.DiscountPrice, productName = eventDetail.Product.Name, origin = p.UnitPrice, pID = productID });
            }

        }

        [HttpPost]
        public ActionResult Save(string description, DateTime startDate, DateTime endDate)
        {
            EventBUS eventBUS = new EventBUS();
            Event newEvent = new Event() { StartDate = startDate, EndDate = endDate, Description = description };
            if (eventBUS.AddEvent(newEvent, this.ListEventDetail) > 0)
            {
                this.ListEventDetail = null;
                TempData["SuccessMsg"] = "Khởi tạo sự kiện thành công";
                return RedirectToAction("Index");
            }
            else
            {
                this.ListEventDetail = null;

                return RedirectToAction("Add");
            }
        
        }

        [HttpPost]
        public ActionResult DeleteDetail(int productID)
        {
            //string productID = Request.Form["productID"];
            var listDetail = this.ListEventDetail;
            EventDetail ed = listDetail.FirstOrDefault(e => e.Product.ID == productID);
            if (ed != null)
            {
                listDetail.Remove(ed);
                this.ListEventDetail = listDetail;
            }
            return Json(new { pID = productID });
        }

        [HttpPost]
        public ActionResult Delete(int evtID) {
            EventBUS eventBUS = new EventBUS();
            if (eventBUS.Delete(evtID) > 0) {
                TempData["SuccessMsg"] = "Xóa thành công";
            }
            else
                TempData["FailMsg"] = "Không thành công";
            return Json(new { redirectToUrl = Url.Action("Index", "Event" )});
        }
    }
}