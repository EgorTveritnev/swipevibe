using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwipeVibe.Web.Controllers
{
    public class UploadController : Controller
    {

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
                    string fileName = Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(fileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + extension;
                    string path = Path.Combine(Server.MapPath("~/App_Data/Files"), uniqueFileName);

                    file.SaveAs(path);
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