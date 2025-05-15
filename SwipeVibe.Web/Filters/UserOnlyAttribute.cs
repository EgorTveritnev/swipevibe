using System.Web;
using System.Web.Mvc;

namespace SwipeVibe.Web.Filters
{
    public class UserOnlyAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var role = httpContext?.Session?["Role"]?.ToString();
            return role == "User" || role == "Admin"; // 👈 допускаем и Admin тоже
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Home/AccessDenied");
        }
    }
}