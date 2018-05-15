using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC学习
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 "MyRoute",
                 "Employee/{EmpId}",
                 new {
                     Controller = "Employee",
                     action = "GetId"
                 },
                 new {EmpId="\\d+"}
                 
                );

            routes.MapRoute(
                name:"Upload",
                url:"Employee/BulkUpload",
                defaults:new {Controller="BulkUpload",action="Index"}
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Employee", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
