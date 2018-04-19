using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using PagedList;
namespace Web.Controllers
{
    public class TimKiemController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: TimKiem
        public ActionResult KQTimKiem(string sTuKhoa, int? page)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pagesize = 4;
            int pagenumber = page ?? 1;
            ViewBag.TuKhoa = sTuKhoa;
            //Tìm kiêm theo tên sp
            var lstSP = db.SanPham.Where(x => x.TenSP.Contains(sTuKhoa));
            return View(lstSP.OrderBy(x => x.TenSP).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult KQTimKiemPartial(string sTuKhoa)
        {
            ViewBag.TuKhoa = sTuKhoa;
            //Tìm kiêm theo tên sp
            var lstSP = db.SanPham.Where(x => x.TenSP.Contains(sTuKhoa));
            return PartialView(lstSP.OrderBy(x=>x.DonGia));
        }
    }
}