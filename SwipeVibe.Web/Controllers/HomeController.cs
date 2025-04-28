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
            ViewBag.Username1 = "username1";
            ViewBag.Username2 = "username2";
            ViewBag.Username3 = "username3";
            ViewBag.Username4 = "username4";
            ViewBag.Username5 = "username5";

            ViewBag.game_lover = "game_lover";
            ViewBag.horror_fan = "horror_fan";
            ViewBag.gaming_geek = "gaming_geek";
            ViewBag.silent_watcher = "silent_watcher";
            ViewBag.game_dev = "game_dev";

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