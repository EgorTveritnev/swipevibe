using System.Web;
using System.Web.Optimization;

namespace SwipeVibe.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/app.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/style.css",
                      "~/Content/css/animations.css",
                      "~/Content/css/theme.css"));

            // Добавляем бандлы для разных страниц
            bundles.Add(new StyleBundle("~/Content/watch").Include("~/Content/css/watch.css"));
            bundles.Add(new StyleBundle("~/Content/upload").Include("~/Content/css/upload.css"));
            bundles.Add(new StyleBundle("~/Content/subscriptions").Include("~/Content/css/subscriptions.css"));
            bundles.Add(new StyleBundle("~/Content/shop").Include("~/Content/css/shop.css"));
            bundles.Add(new StyleBundle("~/Content/friends").Include("~/Content/css/friends.css"));
            bundles.Add(new StyleBundle("~/Content/admin").Include("~/Content/css/admin.css"));
            bundles.Add(new StyleBundle("~/Content/activity").Include("~/Content/css/activity.css"));
            bundles.Add(new StyleBundle("~/Content/auth").Include("~/Content/css/auth.css"));
            bundles.Add(new StyleBundle("~/Content/error").Include("~/Content/css/error.css"));
            bundles.Add(new StyleBundle("~/Content/stream").Include("~/Content/css/stream.css"));

            // Скрипты для разных страниц
            bundles.Add(new ScriptBundle("~/bundles/watch").Include("~/Scripts/watch.js"));
            bundles.Add(new ScriptBundle("~/bundles/upload").Include("~/Scripts/upload.js"));
            bundles.Add(new ScriptBundle("~/bundles/subscriptions").Include("~/Scripts/subscriptions.js"));
            bundles.Add(new ScriptBundle("~/bundles/shop").Include("~/Scripts/shop.js"));
            bundles.Add(new ScriptBundle("~/bundles/friends").Include("~/Scripts/friends.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin").Include("~/Scripts/admin.js"));
            bundles.Add(new ScriptBundle("~/bundles/activity").Include("~/Scripts/activity.js"));
            bundles.Add(new ScriptBundle("~/bundles/auth").Include("~/Scripts/auth.js"));
            bundles.Add(new ScriptBundle("~/bundles/stream").Include("~/Scripts/stream.js"));
        }
    }
} 