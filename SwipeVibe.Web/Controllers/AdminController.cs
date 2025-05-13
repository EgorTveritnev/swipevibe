using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwipeVibe.Web.Models;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.BusinessLogic.BL;

namespace SwipeVibe.Web.Controllers
{
    [Authorize(Roles = "Admin")]    public class AdminController : Controller
    {
        private readonly IVideo _videoService = new VideoBL();
        private readonly UserService _userService = new UserService();
        
        // GET: Admin Dashboard
        public ActionResult Index()
        {
            var videos = _videoService.GetAll().ToList();
            var users = _userService.GetAllUsers().ToList();
            
            var dashboardModel = new AdminDashboardViewModel
            {
                RegisteredUsersCount = users.Count,
                TotalVideosCount = videos.Count,
                ActiveUsersCount = users.Count(u => u.IsActive),
                BlockedUsersCount = users.Count(u => !u.IsActive),
                TodayNewUsersCount = users.Count(u => u.RegisteredDate.Date == DateTime.Today),
                TodayNewVideosCount = videos.Count(v => v.UploadDate.Date == DateTime.Today),
                LatestVideos = videos.OrderByDescending(v => v.UploadDate).Take(5).ToList()
            };

            return View(dashboardModel);
        }

        // GET: Admin/Videos
        public ActionResult Videos()
        {
            var videos = _videoService.GetAll();
            return View(videos);
        }

        // GET: Admin/Users
        public ActionResult Users()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        // POST: Admin/DeleteVideo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVideo(int id)
        {
            try
            {
                _videoService.Delete(id);
                TempData["SuccessMessage"] = "Видео успешно удалено";
                return RedirectToAction("Videos");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ошибка при удалении видео: {ex.Message}";
                return RedirectToAction("Videos");
            }
        }

        // POST: Admin/BlockUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlockUser(int id)
        {
            try
            {
                _userService.ToggleUserStatus(id);
                TempData["SuccessMessage"] = "Статус пользователя изменен";
                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ошибка при изменении статуса пользователя: {ex.Message}";
                return RedirectToAction("Users");
            }
        }
    }
}
