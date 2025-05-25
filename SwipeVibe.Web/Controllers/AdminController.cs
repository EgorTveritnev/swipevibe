using System;
using System.Linq;
using System.Web.Mvc;
using SwipeVibe.BusinessLogic;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Enums;
using SwipeVibe.Domain.Entities.User;
using SwipeVibe.Web.Models;
using System.Dynamic;
using System.Collections.Generic;
using SwipeVibe.Web.Filters;

namespace SwipeVibe.Web.Controllers
{
    [AdminOnly]
    public class AdminController : Controller
    {
        private readonly IAdmin _adminBL;
        private readonly IUser _userBL;
        private readonly IVideo _videoBL;

        public AdminController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _adminBL = bl.Admin;
            _userBL = bl.User;
            _videoBL = bl.Video;
        }

        public ActionResult Index()
        {
            var allUsers = _adminBL.AllUsers();
            var allVideos = _videoBL.GetAll();

            var model = new AdminDashboardViewModel
            {
                RegisteredUsersCount = allUsers.Count,
                ActiveUsersCount = allUsers.Count(u => !u.IsBlocked),
                BlockedUsersCount = allUsers.Count(u => u.IsBlocked),
                TodayNewUsersCount = allUsers.Count(u => u.RegisteredDate.Date == DateTime.Today),

                TotalVideosCount = allVideos.Count(),
                TodayNewVideosCount = allVideos.Count(v => v.UploadDate.Date == DateTime.Today),
                LatestVideos = allVideos.OrderByDescending(v => v.UploadDate)
                                        .Take(5)
                                        .ToList()
            };

            return View(model);
        }

        public ActionResult Users()
        {
            var model = _adminBL.AllUsers()
                .Select(u =>
                {
                    dynamic d = new ExpandoObject();
                    d.Id = u.Id;
                    d.Username = u.Username;
                    d.Email = u.Email;
                    d.AvatarUrl = string.IsNullOrWhiteSpace(u.AvatarUrl)
                                       ? "/content/default-avatar.png"
                                       : u.AvatarUrl;
                    d.RegisteredDate = u.RegisteredDate;
                    d.IsBlocked = u.IsBlocked;
                    d.Role = u.Role;
                    return d;
                })
                .ToList();

            return View(model);
        }

        public ActionResult Videos()
        {
            var model = _videoBL.GetAll()
                .Select(v => new
                {
                    v.Id,
                    v.Username,
                    v.Description,
                    v.UploadDate,
                    v.VideoUrl
                }).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteVideo(int id)
        {
            try
            {
                _videoBL.Delete(id);
                TempData["SuccessMessage"] = "Видео удалено успешно.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ошибка удаления видео: {ex.Message}";
            }
            return RedirectToAction("Videos");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult BlockUser(int id)
        {
            try
            {
                var user = _userBL.ById(id);
                if (user == null) throw new Exception("Пользователь не найден");

                if (user.IsBlocked) _adminBL.Unblock(id);
                else _adminBL.Block(id);

                TempData["SuccessMessage"] = "Статус пользователя изменён";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ошибка изменения статуса: {ex.Message}";
            }
            return RedirectToAction("Users");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangeUserRole(int id)
        {
            try
            {
                var user = _userBL.ById(id);
                if (user == null) throw new Exception("Пользователь не найден");

                if (user.Role == "SuperAdmin")
                    throw new UnauthorizedAccessException("Нельзя изменить роль SuperAdmin");

                var newRole = user.Role == "User" ? Role.Admin : Role.User;
                _adminBL.SetRole(id, newRole);

                TempData["SuccessMessage"] = "Роль пользователя изменена";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ошибка изменения роли: {ex.Message}";
            }
            return RedirectToAction("Users");
        }
    }
}
