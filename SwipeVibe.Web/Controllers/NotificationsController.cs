using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwipeVibe.Web.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        // GET: Notifications
        public ActionResult Index()
        {
            // Логика получения уведомлений пользователя
            // ...
            
            return View();
        }

        // GET: Notifications/MarkAsRead/5
        public ActionResult MarkAsRead(int id)
        {
            // Логика отметки уведомления как прочитанного
            // ...
            
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: Notifications/MarkAllAsRead
        public ActionResult MarkAllAsRead()
        {
            // Логика отметки всех уведомлений как прочитанных
            // ...
            
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(int id)
        {
            // Логика удаления уведомления
            // ...
            
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: Notifications/GetCount
        public ActionResult GetCount()
        {
            // Логика получения количества непрочитанных уведомлений
            // ...
            
            int count = new Random().Next(0, 10); // В реальном приложении здесь будет запрос к БД
            
            return Json(new { count }, JsonRequestBehavior.AllowGet);
        }

        // GET: Notifications/GetLatest
        public ActionResult GetLatest()
        {
            // Логика получения последних уведомлений для выпадающего меню
            // ...
            
            // Пример данных
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