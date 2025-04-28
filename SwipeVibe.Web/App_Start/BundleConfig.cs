using System.Web.Optimization;

namespace SwipeVibe.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            // Общие стили
            bundles.Add(new StyleBundle("~/Content/common-css").Include(
                "~/Content/bootstrap/css/bootstrap.css",
                "~/Content/css/style.css",
                "~/Content/css/animations.css",
                "~/Content/css/theme.css",
                "~/Content/css/neomorphism.css"
            ));

            // Стили для отдельных разделов
            bundles.Add(new StyleBundle("~/Content/auth-css").Include(
                "~/Content/css/auth.css"
            ));

            bundles.Add(new StyleBundle("~/Content/activity-css").Include(
                "~/Content/css/activity.css"
            ));

            bundles.Add(new StyleBundle("~/Content/admin-css").Include(
                "~/Content/css/admin.css"
            ));

            bundles.Add(new StyleBundle("~/Content/error-css").Include(
                "~/Content/css/error.css"
            ));

            bundles.Add(new StyleBundle("~/Content/friends-css").Include(
                "~/Content/css/friends.css"
            ));

            bundles.Add(new StyleBundle("~/Content/shop-css").Include(
                "~/Content/css/shop.css"
            ));

            bundles.Add(new StyleBundle("~/Content/subscriptions-css").Include(
                "~/Content/css/subscriptions.css"
            ));

            bundles.Add(new StyleBundle("~/Content/toast-css").Include(
                "~/Content/css/toast.css"
            ));

            bundles.Add(new StyleBundle("~/Content/upload-css").Include(
                "~/Content/css/upload.css"
            ));

            bundles.Add(new StyleBundle("~/Content/watch-css").Include(
                "~/Content/css/watch.css"
            ));

            // App-specific JS
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app.js"
            ));

            // Modernizr
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Content/modernizr/modernizr.js"
            ));

            // Bootstrap JS
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                "~/Content/bootstrap/js/bootstrap.js"
            ));

            // Скрипты по разделам
            bundles.Add(new ScriptBundle("~/bundles/jsfiles").Include(
                "~/Scripts/watch.js",
                "~/Scripts/upload.js",
                "~/Scripts/subscriptions.js",
                "~/Scripts/shop.js",
                "~/Scripts/friends.js",
                "~/Scripts/admin.js",
                "~/Scripts/activity.js",
                "~/Scripts/auth.js",
                "~/Scripts/stream.js"
            ));
        }
    }
}
