using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MyIdentity
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .Include(
                    "~/scripts/jquery-{version}.min.js",
                    "~/scripts/bootstrap.min.js"
                    ));
            bundles.Add(new StyleBundle("~/Content/css")
                .Include(
                    "~/Content/bootstrap.min.css",
                    "~/Content/Site.css"
                ));
            BundleTable.EnableOptimizations = true;
        }
    }
}