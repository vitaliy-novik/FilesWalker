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
                "FoldersCopyTo",
                "Folders/CopyTo/{*path}",
                new { controller = "Folders", action = "CopyTo" });

            routes.MapRoute(
                "FoldersRename",
                "Folders/Rename/{*path}",
                new { controller = "Folders", action = "Rename" });

            routes.MapRoute(
                "FoldersDelete",
                "Folders/Delete/{*path}",
                new { controller = "Folders", action = "Delete" });

            routes.MapRoute(
                "FoldersCreate",
                "Folders/Create/{*path}",
                new { controller = "Folders", action = "Create" });

            routes.MapRoute(
                "FoldersCreateFile",
                "Folders/CreateFile/{*path}",
                new { controller = "Folders", action = "CreateFile" });

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