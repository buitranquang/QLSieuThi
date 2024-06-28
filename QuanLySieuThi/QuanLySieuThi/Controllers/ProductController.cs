﻿using QuanLySieuThi.BUS;
using QuanLySieuThi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySieuThi.Filter;

namespace QuanLySieuThi.Controllers
{
    [CommonAttributeFilter]
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product(int id)
        {
            ProductBUS bus = new ProductBUS();
            EventBUS eventBUS = new EventBUS();
            Event evt = eventBUS.GetCurrentEvent();
            Product p = bus.GetProduct(id);
            ViewBag.product = p;
            if (evt != null)
            {
                EventDetail detail = evt.EventDetails.FirstOrDefault(d => d.ProductID == p.ID);
                if (detail != null)
                {
                    ViewBag.eventDetail = detail;
                }
            }


            CommentBUS commentBUS = new CommentBUS();

            List<Comment> comments = commentBUS.GetCommentByProductId(id);
            ViewBag.comments = comments;

            return View();
        }
        [HttpPost]
        [AuthenticationFilter]
        public ActionResult Comment(int productID, string comment)
        {
            CommentBUS commentBUS = new CommentBUS();
            Comment c = new Comment()
            {
                ProductID = productID,
                Content = comment,
                CreatedDate = DateTime.Now,
                CustomerID = (Session["currentUser"] as Customer).ID
            };
            commentBUS.AddComment(c);
            return RedirectToAction("Product", "Product", new { id = productID });
        }
    }
}