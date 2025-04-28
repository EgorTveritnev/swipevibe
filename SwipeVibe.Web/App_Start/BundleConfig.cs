using System.Web.Optimization;

namespace SwipeVibe.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                            "~/Scripts/app.js"));

            // Отдельный бандл для кастомных стилей
            bundles.Add(new StyleBundle("~/Content/custom-css").Include(
                            "~/Content/bootstrap/css/bootstrap.css",
                            "~/Content/css/style.css",
                            "~/Content/css/animations.css",
                            "~/Content/css/theme.css",
                            "~/Content/css/neomorphism.css",
                            "~/Content/css/auth.css",
                            "~/Content/css/activity.css",
                            "~/Content/css/admin.css",
                            "~/Content/css/error.css",
                            "~/Content/css/friends.css",
                            "~/Content/css/shop.css",
                            "~/Content/css/subscriptions.css",
                            "~/Content/css/toast.css",
                            "~/Content/css/upload.css",
                            "~/Content/css/watch.css"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Content/modernizr/modernizr.js"));

            //  Bootstrap JavaScript
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                            "~/Content/bootstrap/js/bootstrap.js"));



            bundles.Add(new ScriptBundle("~/bundles/jsfiles").Include(
                "~/Scripts/watch.js",
                "~/Scripts/upload.js",
                "~/Scripts/subscriptions.js",
                "~/Scripts/shop.js",
                "~/Scripts/friends.js",
                "~/Scripts/admin.js",
                "~/Scripts/activity.js",
                "~/Scripts/auth.js",
                "~/Scripts/stream.js"));
        }
    }
}