using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwipeVibe.Web.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Videos(string id)
        {
            ViewBag.CategoryName = id;
            return View();
        }
        public ActionResult Trending(string id)
        {
            ViewBag.CategoryName = id;
            return View();
        }
        public ActionResult New(string id)
        {
            ViewBag.CategoryName = id;
            return View();
        }
        public ActionResult Popular(string id)
        {
            ViewBag.CategoryName = id;
            return View();
        }
    }
}