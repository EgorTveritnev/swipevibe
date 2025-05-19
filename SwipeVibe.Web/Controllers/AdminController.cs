using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using SwipeVibe.Web.Models;
using SwipeVibe.BusinessLogic.BL;
using SwipeVibe.BusinessLogic.Core;
using SwipeVibe.Domain.Enums;
using SwipeVibe.Web.Filters;
using SwipeVibe.Domain.Entities.User;
using System.Collections.Generic;
using System.Dynamic;

namespace SwipeVibe.Web.Controllers
{
    [AdminOnly]
    public class AdminController : Controller
    {
        private readonly AdminApi _adminService;
        private readonly UserApi _userService;
        private readonly VideoBL _videoService;
        private readonly IMapper _mapper;      

        public AdminController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SwipeVibe.Domain.Entities.User.User, UserReturn>();
                cfg.CreateMap<UserRegister, SwipeVibe.Domain.Entities.User.User>();
            });

            _mapper = config.CreateMapper();

            var repo = new UserRepositoryBL();
            var session = new SessionBL();

            _adminService = new AdminApi(repo, _mapper);
            _userService = new UserApi(repo, session, _mapper);
            _videoService = new VideoBL();
        }
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
                LatestVideos = videos.OrderByDescending(v => v.UploadDate)
                                             .Take(5)
                                             .ToList()
            };

            return View(dashboardModel);
        }
        public ActionResult Videos()
        {
            var videoList = _videoService.GetAll()
                                         .Select(v => new
                                         {
                                             v.Id,
                                             v.Username,
                                             v.Description,
                                             v.UploadDate,
                                             v.VideoUrl
                                         })
                                         .ToList();

            return View(videoList);  
        }
        public ActionResult Users()
        {
            var model = _adminService.GetAllUsers()
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
                    d.Role = u.Role.ToString();
                    return d;                    
                })
                .ToList();                    

            return View(model);                  
        }
        [HttpPost, ValidateAntiForgeryToken]
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

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult BlockUser(int id)
        {
            try
            {
                var user = _userService.GetAllUsers().FirstOrDefault(u => u.Id == id);
                if (user == null) throw new Exception("Пользователь не найден");

                if (user.IsBlocked) _adminService.Unblock(id);
                else _adminService.Block(id);

                TempData["SuccessMessage"] = "Статус пользователя изменён";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ошибка при изменении статуса: {ex.Message}";
            }

            return RedirectToAction("Users");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangeUserRole(int id)
        {
            try
            {
                var user = _userService.GetAllUsers().FirstOrDefault(u => u.Id == id);
                if (user == null) throw new Exception("Пользователь не найден");

                if (user.Role == Role.SuperAdmin)
                    throw new UnauthorizedAccessException("Нельзя изменить роль суперадмина");

                var newRole = user.Role == Role.User ? Role.Admin : Role.User;
                _adminService.SetRole(id, newRole);

                TempData["SuccessMessage"] = "Роль пользователя изменена";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ошибка при изменении роли: {ex.Message}";
            }

            return RedirectToAction("Users");
        }
    }
}
