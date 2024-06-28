using QuanLySieuThi.BUS;
using QuanLySieuThi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySieuThi.Filter
{
    public class CommonAttributeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CategoryBUS categoryBUS = new CategoryBUS();
            List<Category> category = categoryBUS.GetCategories();
            filterContext.Controller.ViewBag.Categories = category;
            base.OnActionExecuting(filterContext);
        }
    }
}