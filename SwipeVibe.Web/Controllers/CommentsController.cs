using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwipeVibe.Web.Controllers
{
    public class CommentsController : Controller
    {
        public ActionResult List(int id, int page = 1, string sort = "newest")
        {
            ViewBag.VideoId = id;
            ViewBag.Page = page;
            ViewBag.Sort = sort;
            return PartialView("_CommentsList");
        }
        [HttpPost]
        [Authorize]
        public ActionResult Add(int videoId, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Json(new { success = false, message = "Текст комментария не может быть пустым." });
            }
            var comment = new
            {
                Id = new Random().Next(1000, 9999),
                VideoId = videoId,
                Text = text,
                UserName = User.Identity.Name,
                UserAvatar = "/Content/assets/images/avatar1.jpg",
                CreatedDate = DateTime.Now,
                Likes = 0,
                Dislikes = 0
            };

            return Json(new { success = true, comment });
        }
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Json(new { success = false, message = "Текст комментария не может быть пустым." });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
            return Json(new { success = true });
        }
        [HttpPost]
        [Authorize]
        public ActionResult Like(int id)
        {
            int likes = new Random().Next(10, 100);
            return Json(new { success = true, likes });
        }
        [HttpPost]
        [Authorize]
        public ActionResult Dislike(int id)
        {
            int dislikes = new Random().Next(1, 20);

            return Json(new { success = true, dislikes });
        }

        [HttpPost]
        [Authorize]
        public ActionResult Reply(int parentId, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Json(new { success = false, message = "Текст ответа не может быть пустым." });
            }
            var reply = new
            {
                Id = new Random().Next(1000, 9999),
                ParentId = parentId,
                Text = text,
                UserName = User.Identity.Name,
                UserAvatar = "/Content/assets/images/avatar1.jpg",
                CreatedDate = DateTime.Now,
                Likes = 0,
                Dislikes = 0
            };

            return Json(new { success = true, reply });
        }
    }
}