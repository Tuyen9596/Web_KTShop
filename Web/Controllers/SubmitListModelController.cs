using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
namespace Web.Controllers
{
    public class SubmitListModelController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: SubmitListModel
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(IEnumerable<ChiTietPhieunhap> ModelList)
        {
            return View();
        }
        [HttpGet]
        public ActionResult NhapHang()
        {
            ViewBag.MaNCC =new SelectList( db.NhaCC,"MaNCC","TenNCC");
            ViewBag.ListSanPham =db.SanPham;
            return View();
        }
        [HttpPost]
        public ActionResult NhapHang(PhieuNhap Model,IEnumerable<ChiTietPhieunhap> ModelList)
        {
            if (ModelList != null && Model != null)
            {
                ViewBag.MaNCC = new SelectList(db.NhaCC, "MaNCC", "TenNCC");
                ViewBag.ListSanPham = db.SanPham;
                Model.DaXoa = false;
                db.PhieuNhap.Add(Model);
                db.SaveChanges();
                //savechanges de lay MaPN gan cho ChiTietPN
                SanPham sp;
                foreach (var item in ModelList)
                {
                    //Cap nhat so luong ton
                    sp = db.SanPham.Single(x => x.MaSP == item.MaSP);
                    sp.SoLuongTon += item.SoLuongNhap;
                    item.MaPN = Model.MaPN;
                }
                db.ChiTietPhieunhap.AddRange(ModelList);
                db.SaveChanges();
            }
            return View();
        }
    }
}