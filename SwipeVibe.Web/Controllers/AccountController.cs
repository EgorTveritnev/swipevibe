using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SwipeVibe.BusinessLogic;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.User;
using SwipeVibe.Web.Filters;
using SwipeVibe.Web.Models;

namespace SwipeVibe.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUser _userBL;
        private readonly ISession _sessionBL;

        public AccountController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _userBL = bl.GetUserBL();
            _sessionBL = bl.GetSessionBL();
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

            try
            {
                var loginData = new UserLoginData
                {
                    Email = model.Email,
                    Password = model.Password,
                    LoginIp = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now
                };

                var result = _sessionBL.Login(loginData);

                Session["User"] = result.UserInfo;
                Session["Role"] = result.UserInfo.Role;

                FormsAuthentication.SetAuthCookie(result.UserInfo.Email, model.RememberMe);

                return RedirectToLocal(returnUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        [UserOnly]
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
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
                var regData = new UserRegisterData
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    RegisterTime = DateTime.UtcNow
                };

                var result = _sessionBL.Register(regData);
                TempData["Success"] = "Регистрация успешна!";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
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

            TempData["Message"] = "Ссылка на сброс пароля отправлена (эмуляция)";
            return RedirectToAction("Login");
        }

        [UserOnly]
        public ActionResult Profile()
        {
            if (!(Session["User"] is UserReturn user))
                return RedirectToAction("Login");

            var upToDateUser = _userBL.ById(user.Id);

            ViewBag.Role = upToDateUser.Role;
            ViewBag.Username = upToDateUser.Username;
            ViewBag.AvatarUrl = upToDateUser.AvatarUrl;
            ViewBag.Email = upToDateUser.Email;
            ViewBag.RegisteredDate = upToDateUser.RegisteredDate.ToString("dd.MM.yyyy");

            var bl = new BusinessLogic.BusinessLogic();
            var videoBL = bl.GetVideoBL();
            var videos = videoBL.GetAll()
                .Where(v => v.UserId == upToDateUser.Id)
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
                AuthorId = upToDateUser.Id,
                AuthorName = upToDateUser.Username,
                AuthorAvatarUrl = upToDateUser.AvatarUrl
            }).ToList();

            return View();
        }


        [UserOnly]
        [HttpGet]
        public ActionResult EditProfile()
        {
            if (!(Session["User"] is UserReturn user))
                return RedirectToAction("Login");

            var data = _userBL.ById(user.Id);

            return View(new EditProfileViewModel
            {
                Username = data.Username,
                Email = data.Email,
                CurrentAvatarUrl = data.AvatarUrl
            });
        }

        [UserOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileViewModel model)
        {
            if (!(Session["User"] is UserReturn user))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var upd = new UserUpdate
                {
                    Username = model.Username,
                    Email = model.Email
                };

                _userBL.Update(user.Id, upd);

                if (model.Avatar != null && model.Avatar.ContentLength > 0)
                {
                    var avatarUrl = ProcessAvatarUpload(model.Avatar);
                    _userBL.UpdateAvatar(user.Id, avatarUrl);
                }

                Session["User"] = _userBL.ById(user.Id);

                TempData["Success"] = "Профиль обновлён";
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
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(avatar.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                throw new InvalidOperationException("Недопустимый формат файла");

            if (avatar.ContentLength > 5 * 1024 * 1024)
                throw new InvalidOperationException("Файл слишком большой (макс. 5MB)");

            var fileName = Guid.NewGuid() + extension;
            var path = Path.Combine(Server.MapPath("~/Content/uploads/avatars"), fileName);

            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            avatar.SaveAs(path);
            return "/Content/uploads/avatars/" + fileName;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

    }
}