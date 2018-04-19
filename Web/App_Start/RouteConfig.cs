using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Tao duong dan cho trabg xem chi tiet
            routes.MapRoute(
               name: "xemchitiet",
               url: "Xem-Chi-Tiet",
                defaults: new { controller = "SanPham", action = "Xemchitiet", id = UrlParameter.Optional }
           );
            //Cau hinh duong dan co tham so cho trang xem chi tiet
            routes.MapRoute(
               name: "XemchitietSP",
               url: "{tensp}-{id}",
                defaults: new { controller = "SanPham", action = "Xemchitiet", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                               defaults: new { controller = "HomeLayout", action = "Index", id = UrlParameter.Optional }

            );
        }
    }
}
