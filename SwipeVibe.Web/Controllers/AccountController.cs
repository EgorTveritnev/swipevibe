using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SwipeVibe.Web.Models;
using SwipeVibe.Domain.Entities.User;
using SwipeVibe.BusinessLogic.Interfaces;
using System.Web.Helpers;

namespace SwipeVibe.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService = new UserService();

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
            {
                return View(model);
            }

            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Неверный email или пароль");
                return View(model);
            }
            var ticket = new FormsAuthenticationTicket(
                1, user.Email, DateTime.Now,
                DateTime.Now.AddHours(8),
                model.RememberMe,
                user.Role.ToString());
            var enc = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, enc);
            if (model.RememberMe) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);

            // Сохраняем данные пользователя в сессии
            Session["UserId"] = user.Id;
            Session["Username"] = user.Username;
            Session["UserEmail"] = user.Email;

            // Перенаправляем пользователя на страницу, с которой он пришел, или на главную
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
            {
                return View(model);
            }

            var dto = new UserRegister
            {
                Username = model.Username,
             Email = model.Email,
                Password = model.Password
            }
            ;
            var user = _userService.Register(dto);
            if (user == null)
            {
                ModelState.AddModelError("", "Пользователь с таким email или именем уже существует");
                return View(model);
            }

            // Автоматически входим после регистрации
            FormsAuthentication.SetAuthCookie(user.Email, false);

            // Сохраняем данные пользователя в сессии
            Session["UserId"] = user.Id;
            Session["Username"] = user.Username;
            Session["UserEmail"] = user.Email;
            Session["UserAvatar"] = user.AvatarUrl;

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // В реальном приложении здесь будет отправка email с кодом для сброса пароля
            string resetCode = _userService.GeneratePasswordResetCode(model.Email);

            if (resetCode == null)
            {
                // Не сообщаем о несуществующем email по соображениям безопасности
                ViewBag.SuccessMessage = "Если указанный email зарегистрирован в системе, на него будет отправлена инструкция для сброса пароля";
                return View("ForgotPasswordConfirmation");
            }

            // В демо-версии просто выводим ссылку
            var resetUrl = Url.Action("ResetPassword", "Account", new { email = model.Email, code = resetCode }, protocol: Request.Url.Scheme);
            ViewBag.ResetUrl = resetUrl;

            return View("ForgotPasswordConfirmation");
        }

        [HttpGet]
        public ActionResult ResetPassword(string email, string code)
        {
            var model = new ResetPasswordViewModel
            {
                Email = email,
                Code = code
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool result = _userService.ResetPassword(model.Email, model.Code, model.Password);

            if (!result)
            {
                ModelState.AddModelError("", "Неверный код сброса пароля или истек срок его действия");
                return View(model);
            }

            return RedirectToAction("ResetPasswordConfirmation");
        }

        [HttpGet]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}