using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using PagedList;
namespace Web.Controllers
{

    public class SanPhamController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        //chan user truy cap vao partial
        [ChildActionOnly]
        public ActionResult SanPhamStyle1Partial()
        {
            return PartialView();
        }
        //Xay dung trang xem chi tiet
        public ActionResult Xemchitiet(int? id,string tensp)
        {
            //khong truyen vao id se bao loi
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sp = db.SanPham.SingleOrDefault(m => m.MaSP == id && m.DaXoa==false);
            //neu id khhong tin tai du lieu nao tuong ung tra ve loi khong tim  thay
            if(sp==null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        public ActionResult SanPham(int? maloaisp, int? mansx,int? page)
        {
            if (mansx == null||maloaisp==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            var lstsp= db.SanPham.Where(m => m.MaNSX==mansx && m.MaLoaiSP==maloaisp);
           
            //neu id khhong tin tai du lieu nao tuong ung tra ve loi khong tim  thay
            if (lstsp.Count() == 0)
            {
                return HttpNotFound();
            }
            ViewBag.sp = lstsp;
            int pagesize = 2;
            int pagenumber = page ?? 1;
            ViewBag.MaLoaiSP = maloaisp;
            ViewBag.MaNSX = mansx;
            return View(lstsp.OrderBy(n=>n.MaSP).ToPagedList(pagenumber,pagesize));
        }
    }
}