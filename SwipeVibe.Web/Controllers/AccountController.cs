using System;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using SwipeVibe.Web.Models;
using SwipeVibe.BusinessLogic.Core;
using SwipeVibe.BusinessLogic.BL;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.User;
using System.Web;
using System.Linq;
using System.IO;

namespace SwipeVibe.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserApi _userService;

        public AccountController()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SwipeVibe.Domain.Entities.User.User, UserReturn>();
                cfg.CreateMap<UserRegister, SwipeVibe.Domain.Entities.User.User>();
            }).CreateMapper();

            var repo = new UserRepositoryBL();      // твоя in-memory реализация
            var session = new SessionBL();        // новая реализация без WebSession
            _userService = new UserApi(repo, session, mapper);
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Неверный email или пароль");
                return View(model);
            }

            if (user.IsBlocked)
            {
                ModelState.AddModelError("", "Аккаунт заблокирован");
                return View(model);
            }

            var identityName = string.IsNullOrWhiteSpace(user.Email)
    ? (string.IsNullOrWhiteSpace(user.Username) ? Guid.NewGuid().ToString() : user.Username)
    : user.Email;

            FormsAuthentication.SetAuthCookie(identityName, false);
            var roles = user.Role.ToString();

            var ticket = new FormsAuthenticationTicket(
                1,
                user.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                true,
                roles,
                FormsAuthentication.FormsCookiePath);

            string encrypted = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted)
            {
                HttpOnly = true
            };
            Response.Cookies.Add(cookie);
            return RedirectToLocal(returnUrl);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var userRegister = new UserRegister
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password
                };

                _userService.Register(userRegister);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _userService.GeneratePasswordResetCode(model.Email);
                TempData["Message"] = "Ссылка на сброс пароля отправлена (эмуляция)";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        [Authorize]
        public new ActionResult Profile()
        {
            var email = User.Identity.Name;
            var user = _userService.GetAllUsers().FirstOrDefault(u => u.Email == email);

            if (user == null)
                return RedirectToAction("Login");

            return View(user); // 👉 передаем модель
        }
        [Authorize]
        [HttpGet]
        public ActionResult EditProfile()
        {
            var email = User.Identity.Name;
            var user = _userService.GetAllUsers().FirstOrDefault(u => u.Email == email);

            if (user == null) return RedirectToAction("Login");

            return View(new EditProfileViewModel
            {
                Username = user.Username,
                Email = user.Email,
                CurrentAvatarUrl = user.AvatarUrl
            });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileViewModel model)
        {
            var email = User.Identity.Name;
            var user = _userService.GetAllUsers().FirstOrDefault(u => u.Email == email);

            if (user == null) return RedirectToAction("Login");

            if (model.Avatar != null && model.Avatar.ContentLength > 0)
            {
                var fileName = Guid.NewGuid() + System.IO.Path.GetExtension(model.Avatar.FileName);
                var path = Server.MapPath("~/Content/Avatars/");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                var fullPath = Path.Combine(path, fileName);
                model.Avatar.SaveAs(fullPath);

                user.AvatarUrl = "/Content/Avatars/" + fileName;
            }

            user.Username = model.Username;
            user.Email = model.Email;

            _userService.UpdateProfile(user.Id, new UserUpdate
            {
                Username = model.Username,
                Email = model.Email
            });

            return RedirectToAction("Profile");
        }
    }
}