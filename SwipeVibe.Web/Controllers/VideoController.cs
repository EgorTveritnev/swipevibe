using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwipeVibe.BusinessLogic;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.Video;
using SwipeVibe.Domain.Entities.User;
using SwipeVibe.Web.Filters;
using SwipeVibe.Web.Models;

namespace SwipeVibe.Web.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideo _videoBL;
        private readonly IUser _userBL;

        public VideoController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _videoBL = bl.GetVideoBL();
            _userBL = bl.GetUserBL();
        }

       
        public ActionResult Details(int id)
        {
            var v = _videoBL.GetById(id);
            if (v == null) return HttpNotFound();

            var user = _userBL.ById(v.UserId);
            var model = new VideoViewModel
            {
                Id = v.Id,
                FileUrl = v.FileUrl,
                Title = v.Title,
                Description = v.Description,
                DurationSec = v.DurationSec,
                LikesCount = v.LikesCount,
                CommentsCount = v.CommentsCount,
                SharesCount = v.SharesCount,
                UploadDateUtc = v.UploadDateUtc,
                AuthorId = user?.Id ?? 0,
                AuthorName = user?.Username,
                AuthorAvatarUrl = user?.AvatarUrl
            };

            ViewBag.Video = model;
            return View();
        }

        [UserOnly]
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [UserOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(VideoViewModel model)
        {
            if (!(Session["User"] is UserReturn user))
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                if (model.File == null || model.File.ContentLength == 0)
                    throw new InvalidOperationException("Выберите видео для загрузки!");

                var fileUrl = ProcessVideoUpload(model.File);

                var video = new Video
                {
                    UserId = user.Id,
                    FileUrl = fileUrl,
                    Title = model.UploadTitle,
                    Description = model.UploadDescription,
                    UploadDateUtc = DateTime.UtcNow,
                    DurationSec = 0 
                };

                _videoBL.Add(video);

                TempData["Success"] = "Видео успешно загружено!";
                return RedirectToAction("MyVideos");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        public ActionResult Profile()
        {
            var user = (UserReturn)Session["User"];
            if (user == null) return RedirectToAction("Login", "Account");

            var videos = _videoBL.GetAll()
                .Where(v => v.UserId == user.Id)
                .OrderByDescending(v => v.UploadDateUtc)
                .ToList();

            ViewBag.Videos = videos.Select(v => new VideoViewModel
            {
                Id = v.Id,
                FileUrl = v.FileUrl,
                Title = v.Title,
                Description = v.Description,
                DurationSec = v.DurationSec,
                LikesCount = v.LikesCount,
                CommentsCount = v.CommentsCount,
                SharesCount = v.SharesCount,
                UploadDateUtc = v.UploadDateUtc,
                AuthorId = user.Id,
                AuthorName = user.Username,
                AuthorAvatarUrl = user.AvatarUrl
            }).ToList();
            ViewBag.Username = user.Username;
            ViewBag.AvatarUrl = user.AvatarUrl;
            ViewBag.Role = user.Role;

            return View();
        }
        [UserOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (!(Session["User"] is UserReturn user))
                return RedirectToAction("Login", "Account");

            var video = _videoBL.GetById(id);
            if (video == null) return HttpNotFound();
            if (video.UserId != user.Id) return new HttpUnauthorizedResult();

            try
            {
                _videoBL.Delete(id);
                TempData["Success"] = "Видео удалено";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("MyVideos");
        }

        private string ProcessVideoUpload(HttpPostedFileBase file)
        {
            var allowedExtensions = new[] { ".mp4", ".webm", ".mov", ".avi" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                throw new InvalidOperationException("Недопустимый формат видео");

            if (file.ContentLength > 1024 * 1024 * 200) 
                throw new InvalidOperationException("Файл слишком большой (макс. 200MB)");

            var fileName = Guid.NewGuid() + extension;
            var path = Path.Combine(Server.MapPath("~/Content/videos"), fileName);

            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            file.SaveAs(path);
            return "/Content/videos/" + fileName;
        }
    }
}
