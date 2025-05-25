using System;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using SwipeVibe.Web.Models;
using SwipeVibe.BusinessLogic.BL;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.User;
using System.Web;
using System.Linq;
using SwipeVibe.Web.Filters;
using System.IO;

namespace SwipeVibe.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUser _accountService;        public AccountController()
        {
            // Настройка AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SwipeVibe.Domain.Entities.User.User, UserReturn>()
                   .ForMember(d => d.Role, o => o.MapFrom(s => s.Role.ToString()));
                cfg.CreateMap<UserRegister, SwipeVibe.Domain.Entities.User.User>();
            });
            var mapper = mapperConfig.CreateMapper();

            // Создание зависимостей
            var repo = new UserRepositoryBL();
            var session = new SessionBL();
            
            // Создание экземпляра AccountBL
            _accountService = new AccountBL(repo, session, mapper);
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = _accountService.Authenticate(model.Email, model.Password);

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

                Session["Role"] = user.Role;

                var identityName = string.IsNullOrWhiteSpace(user.Email)
                    ? (string.IsNullOrWhiteSpace(user.Username) ? Guid.NewGuid().ToString() : user.Username)
                    : user.Email;

                FormsAuthentication.SetAuthCookie(identityName, false);
                var roles = user.Role;

                var ticket = new FormsAuthenticationTicket(
                    1,
                    user.Email,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
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
        }        [HttpPost]
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

                _accountService.Register(userRegister);
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
                _accountService.GeneratePasswordResetCode(model.Email);
                TempData["Message"] = "Ссылка на сброс пароля отправлена (эмуляция)";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public new ActionResult Profile()
        {
            var email = User.Identity.Name;
            var user = _accountService.GetAllUsers().FirstOrDefault(u => u.Email == email);

            if (user == null)
                return RedirectToAction("Login");

            ViewBag.Username = user.Username;
            ViewBag.Email = user.Email;
            ViewBag.AvatarUrl = string.IsNullOrWhiteSpace(user.AvatarUrl)
                ? "https://cdn-icons-png.flaticon.com/512/4140/4140037.png"
                : user.AvatarUrl;
            ViewBag.Role = user.Role.ToString();
            ViewBag.RegisteredDate = user.RegisteredDate.ToString("dd.MM.yyyy");

            return View();
        }

        [UserOnly]
        [HttpGet]
        public ActionResult EditProfile()
        {
            var email = User.Identity.Name;
            var user = _accountService.GetAllUsers().FirstOrDefault(u => u.Email == email);

            if (user == null) return RedirectToAction("Login");

            return View(new EditProfileViewModel
            {
                Username = user.Username,
                Email = user.Email,
                CurrentAvatarUrl = user.AvatarUrl
            });
        }        [UserOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileViewModel model)
        {
            var email = User.Identity.Name;
            var user = _accountService.GetAllUsers().FirstOrDefault(u => u.Email == email);

            if (user == null) return RedirectToAction("Login");

            try
            {
                var userUpdate = new UserUpdate
                {
                    Username = model.Username,
                    Email = model.Email
                };

                // Обновляем основные данные профиля
                _accountService.UpdateProfile(user.Id, userUpdate);

                // Обрабатываем аватар отдельно, если он загружен
                if (model.Avatar != null && model.Avatar.ContentLength > 0)
                {
                    var avatarUrl = ProcessAvatarUpload(model.Avatar);
                    _accountService.UpdateAvatar(user.Id, avatarUrl);
                }

                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        private string ProcessAvatarUpload(HttpPostedFileBase avatar)
        {
            // Валидация файла
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(avatar.FileName).ToLower();
            
            if (!allowedExtensions.Contains(fileExtension))
                throw new InvalidOperationException("Недопустимый формат файла. Разрешены только JPG, PNG, GIF");

            if (avatar.ContentLength > 5 * 1024 * 1024) // 5MB
                throw new InvalidOperationException("Размер файла не должен превышать 5MB");

            // Создаем уникальное имя файла
            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var uploadPath = Path.Combine(Server.MapPath("~/"), "Content", "uploads", "avatars");
            
            // Создаем папку если её нет
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, fileName);
            avatar.SaveAs(filePath);

            // Возвращаем относительный путь для веб-приложения
            return "/Content/uploads/avatars/" + fileName;
        }
    }
}