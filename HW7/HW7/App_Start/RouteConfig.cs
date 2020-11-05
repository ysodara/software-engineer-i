using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HW7
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Commits For a Sinle Repositories",
                url: "api/commits/{id}",
                defaults: new { controller = "Home", action = "Commits", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "All Repositories",
                url: "api/repositories",
                defaults: new { controller = "Home", action = "UserRepo", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "User Profile",
                url: "api/user",
                defaults: new { controller = "Home", action = "UserProfile", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
