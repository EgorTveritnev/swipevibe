using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwipeVibe.Web.Models;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.BusinessLogic.BL;

namespace SwipeVibe.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVideo _videoService = new VideoBL();
        public ActionResult Index()
        {
            var videos = _videoService.GetAll(); 
            return View(videos);                 
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
        
        public ActionResult Upload()
        {
            ViewBag.Message = "Загрузка нового видео";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(VideoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
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
                    
                    string fileName = Guid.NewGuid().ToString();
                    
                    if (model.VideoFile != null && model.VideoFile.ContentLength > 0)
                    {
                        string fileExtension = Path.GetExtension(model.VideoFile.FileName);
                        string videoPath = Path.Combine(videoFolder, fileName + fileExtension);
                        model.VideoFile.SaveAs(videoPath);
                        model.VideoPath = "/Content/Videos/" + fileName + fileExtension;
                    }

                    if (model.ThumbnailImage != null && model.ThumbnailImage.ContentLength > 0)
                    {
                        string fileExtension = Path.GetExtension(model.ThumbnailImage.FileName);
                        string thumbnailPath = Path.Combine(thumbnailFolder, fileName + fileExtension);
                        model.ThumbnailImage.SaveAs(thumbnailPath);
                        model.ThumbnailPath = "/Content/Thumbnails/" + fileName + fileExtension;
                    }
                    
                    model.UploadDate = DateTime.Now;
                    model.Username = "user123"; 
                    model.UserId = "1";
                    
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
        public ActionResult AccessDenied()
        {
            return View();
        }
    }

}