using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwipeVibe.Web.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments/List/5
        public ActionResult List(int id, int page = 1, string sort = "newest")
        {
            ViewBag.VideoId = id;
            ViewBag.Page = page;
            ViewBag.Sort = sort;
            
            // Логика получения комментариев для видео
            // ...
            
            return PartialView("_CommentsList");
        }

        // POST: Comments/Add
        [HttpPost]
        [Authorize]
        public ActionResult Add(int videoId, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Json(new { success = false, message = "Текст комментария не может быть пустым." });
            }
            
            // Логика добавления комментария
            // ...
            
            // Симуляция добавления комментария для примера
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

        // POST: Comments/Edit
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Json(new { success = false, message = "Текст комментария не может быть пустым." });
            }
            
            // Логика редактирования комментария
            // ...
            
            return Json(new { success = true });
        }

        // POST: Comments/Delete
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
            // Логика удаления комментария
            // ...
            
            return Json(new { success = true });
        }

        // POST: Comments/Like
        [HttpPost]
        [Authorize]
        public ActionResult Like(int id)
        {
            // Логика добавления лайка к комментарию
            // ...
            
            // Симуляция количества лайков
            int likes = new Random().Next(10, 100);
            
            return Json(new { success = true, likes });
        }

        // POST: Comments/Dislike
        [HttpPost]
        [Authorize]
        public ActionResult Dislike(int id)
        {
            // Логика добавления дизлайка к комментарию
            // ...
            
            // Симуляция количества дизлайков
            int dislikes = new Random().Next(1, 20);
            
            return Json(new { success = true, dislikes });
        }

        // POST: Comments/Reply
        [HttpPost]
        [Authorize]
        public ActionResult Reply(int parentId, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Json(new { success = false, message = "Текст ответа не может быть пустым." });
            }
            
            // Логика добавления ответа на комментарий
            // ...
            
            // Симуляция добавления ответа для примера
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