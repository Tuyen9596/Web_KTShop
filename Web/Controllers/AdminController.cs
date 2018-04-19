using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
namespace Web.Controllers
{
    public class AdminController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View(db.SanPham.Where(x=>x.DaXoa==false));
        }
        [HttpGet]
        public ActionResult TaoMoi()
        {
            //truyền dropdownlist qua bên Vỉew
            ViewBag.MaNCC = new SelectList(db.NhaCC.OrderBy(x=>x.MaNCC),"MaNCC","TenNCC");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPham.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuat.OrderBy(x => x.MaNSX), "MaNSX", "TenNSX");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(SanPham sp ,HttpPostedFileBase[]  HinhAnh)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCC.OrderBy(x => x.MaNCC), "MaNCC", "TenNCC",sp.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPham.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai",sp.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuat.OrderBy(x => x.MaNSX), "MaNSX", "TenNSX",sp.MaNSX);
            //kiêm tra hinh ảnh đa tôn tai chưa
            for (int i = 0; i < 4; i++) {
                if (HinhAnh[i].ContentLength > 0)
                {
                    var filename = Path.GetFileName(HinhAnh[0].FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.upload[i] = "Hinh Anh Đã Tồn Tại";
                        return View();
                    }
                    else
                    {
                        HinhAnh[0].SaveAs(path);
                        sp.HinhAnh = HinhAnh[0].FileName;
                    }
                    db.SanPham.Add(sp);
                    db.SaveChanges();
                } }
            return RedirectToAction("Index");
        }
    }
}