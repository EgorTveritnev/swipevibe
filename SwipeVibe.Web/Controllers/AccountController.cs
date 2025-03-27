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
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password, bool rememberMe, string returnUrl)
        {
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
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string username, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Пароли не совпадают.");
                return View();
            }

            FormsAuthentication.SetAuthCookie(username, false);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string email)
        {
            TempData["Message"] = "Инструкции по восстановлению пароля отправлены на ваш email.";
            TempData["Success"] = true;
            return RedirectToAction("Login");
        }
        public ActionResult ResetPassword(string token)
        {
            if (String.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            ViewBag.Token = token;
            return View();
        }
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
            TempData["Message"] = "Ваш пароль был успешно изменен. Теперь вы можете войти с новым паролем.";
            TempData["Success"] = true;

            return RedirectToAction("Login");
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
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
            TempData["Message"] = "Ваш пароль был успешно изменен.";
            TempData["Success"] = true;
            return RedirectToAction("Index", "Settings");
        }
    }
}