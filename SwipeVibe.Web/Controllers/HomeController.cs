using System;
using System.Linq;
using System.Web.Mvc;
using SwipeVibe.BusinessLogic;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Web.Models;

namespace SwipeVibe.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVideo _videoBL;
        private readonly IUser _userBL;

        public HomeController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _videoBL = bl.GetVideoBL();
            _userBL = bl.GetUserBL();
        }

        public ActionResult Index()
        {
            var videos = _videoBL.GetAll()
                .OrderByDescending(v => v.UploadDateUtc)
                .ToList();

            var videoList = videos.Select(v =>
            {
                var user = _userBL.ById(v.UserId);
                return new VideoViewModel
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
            }).ToList();

            ViewBag.Videos = videoList;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Информация о проекте";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Контакты";
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

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
