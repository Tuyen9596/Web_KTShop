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
        [ChildActionOnly]
        public ActionResult MenuLeftPartial()
        {
            var lst=db.SanPham.OrderBy(x => x. LuotBinhChon);
            return PartialView(lst);
        }
        //Xay dung trang xem chi tiet
        public ActionResult Xemchitiet(int? id, string tensp)
        {
            //khong truyen vao id se bao loi
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sp = db.SanPham.SingleOrDefault(m => m.MaSP == id && m.DaXoa == false);
            //neu id khhong tin tai du lieu nao tuong ung tra ve loi khong tim  thay
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        public ActionResult SanPham(int? maloaisp, int? mansx, int? page)
        {
            int pagesize = 8;
            int pagenumber = page ?? 1;
            if (mansx == null )
            {
                var lstSP = db.SanPham.Where(m => m.MaLoaiSP == maloaisp);
                return View(lstSP.OrderBy(n => n.MaSP).ToPagedList(pagenumber, pagesize));
            }
            if (maloaisp == null)
            {
                var lstSP = db.SanPham.Where(m => m.MaNSX == mansx);
                return View(lstSP.OrderBy(n => n.MaSP).ToPagedList(pagenumber, pagesize));
            }

            var lstsp = db.SanPham.Where(m => m.MaNSX == mansx && m.MaLoaiSP == maloaisp);

            //neu id khhong tin tai du lieu nao tuong ung tra ve loi khong tim  thay
            if (lstsp.Count() == 0)
            {
                return HttpNotFound();
            }
            ViewBag.sp = lstsp;
            ViewBag.lstSP = db.SanPham;
           
            ViewBag.MaLoaiSP = maloaisp;
            ViewBag.MaNSX = mansx;
            return View(lstsp.OrderBy(n => n.MaSP).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult MenuProductLeft()
        {
            return View(db.LoaiSanPham);
        }
    }
}