using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Text;
using System.Web.Security;

namespace SwipeVibe.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.UTF8;
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var cookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null) return;

            FormsAuthenticationTicket ticket;
            try { ticket = FormsAuthentication.Decrypt(cookie.Value); }
            catch { return; }

            // userData Ч строка ЂAdminї или ЂUserї
            var roles = new[] { ticket.UserData };
            var identity = new FormsIdentity(ticket);
            var principal = new System.Security.Principal.GenericPrincipal(identity, roles);
            Context.User = principal;     // ? теперь User.IsInRole(...) работает
        }
    }
}
