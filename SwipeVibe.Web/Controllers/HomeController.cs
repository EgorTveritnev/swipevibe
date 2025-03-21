using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwipeVibe.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "SwipeVibe - ваша платформа для обмена видео.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Свяжитесь с нами.";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
} 