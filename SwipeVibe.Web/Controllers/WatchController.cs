using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwipeVibe.Web.Controllers
{
    public class WatchController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Video(int id)
        {
            ViewBag.VideoId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Rate(int id, int rating)
        {

            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult Comment(int videoId, string commentText)
        {
            var comment = new
            {
                Id = new Random().Next(1000, 9999),
                VideoId = videoId,
                Text = commentText,
                UserName = "Текущий пользователь",
                UserAvatar = "/Content/assets/images/avatar1.jpg",
                CreatedDate = DateTime.Now
            };

            return Json(new { success = true, comment });
        }

        public ActionResult History()
        {
            return View();
        }
        public ActionResult Related(int id)
        {
            return PartialView("_RelatedVideos");
        }
    }
}