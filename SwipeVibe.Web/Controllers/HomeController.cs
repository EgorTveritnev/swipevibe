using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwipeVibe.Web.Models;

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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult Discover()
        {
            ViewBag.Message = "Откройте новые видео";
            
            return View();
        }
        
        public ActionResult Notifications()
        {
            ViewBag.Message = "Ваши уведомления";
            
            return View();
        }
        
        public new ActionResult Profile()
        {
            ViewBag.Message = "Ваш профиль";
            
            return View();
        }
        
        // GET: Home/Upload
        public ActionResult Upload()
        {
            ViewBag.Message = "Загрузка нового видео";
            return View();
        }
        
        // POST: Home/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(VideoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Создаем папки для хранения файлов, если они не существуют
                    string videoFolder = Server.MapPath("~/Content/Videos");
                    string thumbnailFolder = Server.MapPath("~/Content/Thumbnails");
                    
                    if (!Directory.Exists(videoFolder))
                    {
                        Directory.CreateDirectory(videoFolder);
                    }
                    
                    if (!Directory.Exists(thumbnailFolder))
                    {
                        Directory.CreateDirectory(thumbnailFolder);
                    }
                    
                    // Генерируем уникальное имя файла
                    string fileName = Guid.NewGuid().ToString();
                    
                    // Обработка видеофайла
                    if (model.VideoFile != null && model.VideoFile.ContentLength > 0)
                    {
                        string fileExtension = Path.GetExtension(model.VideoFile.FileName);
                        string videoPath = Path.Combine(videoFolder, fileName + fileExtension);
                        model.VideoFile.SaveAs(videoPath);
                        model.VideoPath = "/Content/Videos/" + fileName + fileExtension;
                    }
                    
                    // Обработка изображения-обложки, если оно было загружено
                    if (model.ThumbnailImage != null && model.ThumbnailImage.ContentLength > 0)
                    {
                        string fileExtension = Path.GetExtension(model.ThumbnailImage.FileName);
                        string thumbnailPath = Path.Combine(thumbnailFolder, fileName + fileExtension);
                        model.ThumbnailImage.SaveAs(thumbnailPath);
                        model.ThumbnailPath = "/Content/Thumbnails/" + fileName + fileExtension;
                    }
                    
                    // Заполняем другие свойства модели
                    model.UploadDate = DateTime.Now;
                    model.Username = "user123"; // В реальном приложении будет использоваться текущий пользователь
                    model.UserId = "1"; // Идентификатор пользователя из системы аутентификации
                    
                    // TODO: Сохранение модели в базу данных
                    
                    TempData["SuccessMessage"] = "Видео успешно загружено!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Произошла ошибка при загрузке видео: " + ex.Message);
                }
            }
            
            return View(model);
        }
    }
}