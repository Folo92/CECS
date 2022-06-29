using System.Web;
using System.Web.Optimization;

namespace CECS
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        // "~/bundles/jquery"表示分组路径（虚拟路径）
        // @Styles.Render("~/Content/css") 页面中加载时用虚拟的分组路径
        // @Scripts.Render("~/bundles/modernizr")
        // "~/Scripts/jquery-{version}.js" 加上{version}可以直接从NuGet更新到最新版jQuery
        // "~/Scripts/jquery.validate*" *号是通配符
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
                        "~/Scripts/jquery.easyui.min.js",
                        "~/Scripts/easyui-lang-zh_CN.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/wy.kit.js",
                        "~/Scripts/wikmenu.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            /////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //分开并单独定义虚拟路径
            //因为css内容中包含地址url(img/bg.png)，与不同路径的css文件组合打包压缩时路径会出问题
            bundles.Add(new StyleBundle("~/Content/easyui").Include("~/Content/easyui/themes/default/easyui.css"));
            bundles.Add(new StyleBundle("~/Content/easyui_icon").Include("~/Content/easyui/themes/icon.css"));
            bundles.Add(new StyleBundle("~/Content/custom").Include("~/Content/default.css"));

            //BundleTable.EnableOptimizations = true; //手动开启捆绑压缩模式
        }
    }
}
