using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using SwipeVibe.Web.Models;
using SwipeVibe.BusinessLogic.BL;
using SwipeVibe.BusinessLogic.Core;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AdminApi _adminService;
        private readonly UserApi _userService;
        private readonly VideoBL _videoService;

        public AdminController()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SwipeVibe.Domain.Entities.User.User, UserReturn>();
                cfg.CreateMap<UserRegister, SwipeVibe.Domain.Entities.User.User>();
            }).CreateMapper();

            var repo = new UserRepository();
            var session = new SessionBL(); // ← используется, чтобы UserApi не вылетал
            _adminService = new AdminApi(repo, mapper);
            _userService = new UserApi(repo, session, mapper);
            _videoService = new VideoBL();
        }

        // Главная панель администратора
        public ActionResult Index()
        {
            var videos = _videoService.GetAll().ToList();
            var users = _userService.GetAllUsers().ToList();

            var dashboardModel = new AdminDashboardViewModel
            {
                RegisteredUsersCount = users.Count,
                TotalVideosCount = videos.Count,
                ActiveUsersCount = users.Count(u => !u.IsBlocked),
                BlockedUsersCount = users.Count(u => u.IsBlocked),
                TodayNewUsersCount = users.Count(u => u.RegisteredDate.Date == DateTime.Today),
                TodayNewVideosCount = videos.Count(v => v.UploadDate.Date == DateTime.Today),
                LatestVideos = videos.OrderByDescending(v => v.UploadDate).Take(5).ToList()
            };

            return View(dashboardModel);
        }

        // Все видео
        public ActionResult Videos()
        {
            var videos = _videoService.GetAll();
            return View(videos);
        }

        // Все пользователи
        public ActionResult Users()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        // Удаление видео
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVideo(int id)
        {
            try
            {
                _videoService.Delete(id);
                TempData["SuccessMessage"] = "Видео успешно удалено";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ошибка при удалении видео: {ex.Message}";
            }

            return RedirectToAction("Videos");
        }

        // Блокировка или разблокировка пользователя
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlockUser(int id)
        {
            try
            {
                var user = _userService.GetAllUsers().FirstOrDefault(u => u.Id == id);
                if (user == null)
                    throw new Exception("Пользователь не найден");

                if (user.IsBlocked)
                    _adminService.Unblock(id);
                else
                    _adminService.Block(id);

                TempData["SuccessMessage"] = "Статус пользователя изменен";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ошибка при изменении статуса пользователя: {ex.Message}";
            }

            return RedirectToAction("Users");
        }
    }
}
