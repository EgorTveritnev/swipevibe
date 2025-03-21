using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SwipeVibe.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password, bool rememberMe, string returnUrl)
        {
            // В реальном приложении здесь будет проверка учетных данных
            if (username == "demo" && password == "password")
            {
                FormsAuthentication.SetAuthCookie(username, rememberMe);
                
                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            
            ModelState.AddModelError("", "Неверное имя пользователя или пароль.");
            return View();
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string username, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Пароли не совпадают.");
                return View();
            }
            
            // В реальном приложении здесь будет регистрация пользователя
            
            // После успешной регистрации сразу авторизуем пользователя
            FormsAuthentication.SetAuthCookie(username, false);
            
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string email)
        {
            // Логика восстановления пароля
            // В реальном приложении здесь будет отправка письма
            
            TempData["Message"] = "Инструкции по восстановлению пароля отправлены на ваш email.";
            TempData["Success"] = true;
            
            return RedirectToAction("Login");
        }

        // GET: Account/ResetPassword
        public ActionResult ResetPassword(string token)
        {
            if (String.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }
            
            ViewBag.Token = token;
            return View();
        }

        // POST: Account/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string token, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Пароли не совпадают.");
                ViewBag.Token = token;
                return View();
            }
            
            // Логика сброса пароля
            // ...
            
            TempData["Message"] = "Ваш пароль был успешно изменен. Теперь вы можете войти с новым паролем.";
            TempData["Success"] = true;
            
            return RedirectToAction("Login");
        }

        // GET: Account/ChangePassword
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Новые пароли не совпадают.");
                return View();
            }
            
            // Логика изменения пароля
            // ...
            
            TempData["Message"] = "Ваш пароль был успешно изменен.";
            TempData["Success"] = true;
            
            return RedirectToAction("Index", "Settings");
        }
    }
} 