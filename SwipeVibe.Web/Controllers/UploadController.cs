using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace SwipeVibe.Web.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, string title, string description, string tags, string category)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    // Сохраняем файл в директорию App_Data/Files
                    string fileName = Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(fileName);
                    
                    // Генерируем уникальное имя файла
                    string uniqueFileName = Guid.NewGuid().ToString() + extension;
                    string path = Path.Combine(Server.MapPath("~/App_Data/Files"), uniqueFileName);
                    
                    file.SaveAs(path);

                    // Здесь будет логика сохранения информации о видео в базу данных
                    // ...

                    TempData["Message"] = "Видео успешно загружено!";
                    TempData["Success"] = true;
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Ошибка при загрузке видео: " + ex.Message;
                    TempData["Success"] = false;
                }
            }
            else
            {
                TempData["Message"] = "Пожалуйста, выберите файл для загрузки.";
                TempData["Success"] = false;
            }

            return RedirectToAction("Index");
        }
    }
} 