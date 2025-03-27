using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwipeVibe.Web.Controllers
{
    public class NotificationsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MarkAsRead(int id)
        {
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MarkAllAsRead()
        {
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCount()
        {
            int count = new Random().Next(0, 10);
            return Json(new { count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLatest()
        {
            var notifications = new[]
            {
                new { id = 1, text = "Пользователь подписался на вас", isRead = false, date = DateTime.Now.AddMinutes(-5) },
                new { id = 2, text = "Ваше видео набрало 1000 просмотров", isRead = false, date = DateTime.Now.AddHours(-2) },
                new { id = 3, text = "Новый комментарий к вашему видео", isRead = true, date = DateTime.Now.AddDays(-1) }
            };

            return Json(notifications, JsonRequestBehavior.AllowGet);
        }
    }
}