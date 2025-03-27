using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwipeVibe.Web.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index(string query, string type = "all", int page = 1)
        {
            ViewBag.Query = query;
            ViewBag.Type = type;
            ViewBag.Page = page;
            return View();
        }
        public ActionResult Autocomplete(string term)
        {
            var suggestions = new[]
            {
                term + " видео",
                term + " канал",
                term + " плейлист",
                "популярное " + term,
                "новое " + term
            };

            return Json(suggestions, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Advanced()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Filter(string query, string type, string dateRange, string duration, string sort)
        {
            return RedirectToAction("Index", new { query, type });
        }
    }
}