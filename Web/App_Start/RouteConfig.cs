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
            //goi file capctcha
            routes.IgnoreRoute("{*Botdetect}",
                new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });
            //Tao duong dan cho trabg San Pham
            // routes.MapRoute(
            //    name: "XemTenSanPham",
            //    url: "{maloaisp}",
            //     defaults: new { controller = "SanPham", action = "SanPham", id = UrlParameter.Optional }
            //);
            //  routes.MapRoute(
            //    name: "XemSanPhamThuongHieu",
            //    url: "{maloaisp}-{mansx}",
            //     defaults: new { controller = "SanPham", action = "SanPham", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
          name: "BrowserOder",
          url: "Browser-oder-{id}",
           defaults: new { controller = "QuanLySanPham", action = "DuyetDonHang", id = UrlParameter.Optional }
      );
            routes.MapRoute(
           name: "ChuaGiao_ThanhToan",
           url: "Did-give-paid",
            defaults: new { controller = "QuanLySanPham", action = "DaGiaoDaThanhToan", id = UrlParameter.Optional }
       );
            routes.MapRoute(
            name: "ChuaThanhToan",
            url: "not-give",
             defaults: new { controller = "QuanLySanPham", action = "ChuaGiao", id = UrlParameter.Optional }
        );
            routes.MapRoute(
              name: "ChuaGiao",
              url: "Unpaid",
               defaults: new { controller = "QuanLySanPham", action = "ChuaThanhToan", id = UrlParameter.Optional }
          );
            routes.MapRoute(
               name: "QuanLySanPham",
               url: "ManagerProduct",
                defaults: new { controller = "QuanLySanPham", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "ChinhSuaSanPham",
               url: "EditProduct-{id}",
                defaults: new { controller = "QuanLySanPham", action = "ChinhSua", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "DangKy",
                url: "register",
                defaults: new { controller = "HomeLayout", action = "DangKy", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "XemSanPham",
               url: "{maloaisp}-{mansx}",
                defaults: new { controller = "SanPham", action = "SanPham", id = UrlParameter.Optional }
           );
            //Cau hinh duong dan co tham so cho trang xem chi tiet
            //routes.MapRoute(
            //   name: "XemchitietSP",
            //   url: "{id}-{tensp}",
            //    defaults: new { controller = "SanPham", action = "Xemchitiet", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
                name: "XemChiTietSP",
                url: "Product/Details-{id}-{tensp}",
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
