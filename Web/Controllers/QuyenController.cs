using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
namespace Web.Controllers
{
    public class QuyenController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: Quyen
        public ActionResult Index()
        {
            return View(db.Quyen.OrderBy(x=>x.TenQuyen));
        }
        [HttpGet]
        public ActionResult PhanQuyen(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            LoaiThanhVien ltv = db.LoaiThanhVien.SingleOrDefault(n => n.MaLoaiTV == id);
            if (ltv == null) return HttpNotFound();
            ViewBag.Quyen = db.Quyen;
            ViewBag.LoaiTVQuyen = db.LoaiThanhVien_Quyen.Where(n => n.MaLoaiTV == id);
            return View(ltv);
        }
    }
}