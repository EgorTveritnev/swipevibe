using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using SwipeVibe.Web.Models;
using SwipeVibe.BusinessLogic.BL;
using SwipeVibe.BusinessLogic.Core;
using SwipeVibe.Domain.Entities.User;
using SwipeVibe.Web.Filters;
using SwipeVibe.Domain.Enums;

namespace SwipeVibe.Web.Controllers
{
    [AdminOnly]
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

            var repo = new UserRepositoryBL();
            var session = new SessionBL(); 
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
        public ActionResult Videos()
        {
            var videos = _videoService.GetAll();
            return View(videos);
        }
        public ActionResult Users()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeUserRole(int id)
        {
            try
            {
                var user = _userService.GetAllUsers().FirstOrDefault(u => u.Id == id);
                if (user == null)
                    throw new Exception("Пользователь не найден");

                if (user.Role == Role.SuperAdmin)
                throw new UnauthorizedAccessException("Нельзя изменить роль суперадмина");
                var newRole = user.Role == Role.User
                                          ? Role.Admin
                                          : Role.User;
                _adminService.SetRole(id, newRole);

                TempData["SuccessMessage"] = "Роль пользователя успешно изменена";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ошибка при изменении роли пользователя: {ex.Message}";
            }

            return RedirectToAction("Users");
        }
    }
}
