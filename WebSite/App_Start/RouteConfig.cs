using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "FoldersCreate",
                "Folders/Create/{*path}",
                new { controller = "Folders", action = "Create" });

            routes.MapRoute(
                "FoldersIndex",
                "Folders/Index",
                new { controller = "Folders", action = "Index" });

            routes.MapRoute(
                "Folders",
                "Folders/{*path}",
                new { controller = "Folders", action = "Folder" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Folders", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}