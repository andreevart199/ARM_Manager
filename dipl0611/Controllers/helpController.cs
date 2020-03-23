using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dipl0611.Controllers
{
    public class helpController : Controller
    {
        // GET: help
        public ActionResult Index()
        {
            string src1 = UrlHelper.GenerateContentUrl("~/Content/1.jpg", this.HttpContext);
            ViewBag.src1 = src1;
            string src2 = UrlHelper.GenerateContentUrl("~/Content/2.jpg", this.HttpContext);
            ViewBag.src2 = src2;
            string src3 = UrlHelper.GenerateContentUrl("~/Content/3.jpg", this.HttpContext);
            ViewBag.src3 = src3;
            string src4 = UrlHelper.GenerateContentUrl("~/Content/4.jpg", this.HttpContext);
            ViewBag.src4 = src4;
            string src5 = UrlHelper.GenerateContentUrl("~/Content/5.jpg", this.HttpContext);
            ViewBag.src5 = src5;
            string src6 = UrlHelper.GenerateContentUrl("~/Content/6.jpg", this.HttpContext);
            ViewBag.src6 = src6;
            string src7 = UrlHelper.GenerateContentUrl("~/Content/7.jpg", this.HttpContext);
            ViewBag.src7 = src7;
            string src8 = UrlHelper.GenerateContentUrl("~/Content/8.jpg", this.HttpContext);
            ViewBag.src8 = src8;
            string src9 = UrlHelper.GenerateContentUrl("~/Content/9.jpg", this.HttpContext);
            ViewBag.src9 = src9;
            string src10 = UrlHelper.GenerateContentUrl("~/Content/10.jpg", this.HttpContext);
            ViewBag.src10 = src10;
            string src11 = UrlHelper.GenerateContentUrl("~/Content/11.jpg", this.HttpContext);
            ViewBag.src11 = src11;
            string src12 = UrlHelper.GenerateContentUrl("~/Content/12.jpg", this.HttpContext);
            ViewBag.src12 = src12;
            string src13 = UrlHelper.GenerateContentUrl("~/Content/13.jpg", this.HttpContext);
            ViewBag.src13 = src13;
            return View();
        }
    }
}