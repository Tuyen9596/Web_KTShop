using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
namespace Web.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: GioHang
        public List<ItemGioHang> LayGioHang()
        {
            //gio hang da ton tai
            List<ItemGioHang> lstgiohang = (List<ItemGioHang>)Session["GioHang"];
            if (Session["GioHang"] == null)
            {
                //gio hang chua ton tai
                lstgiohang = new List<ItemGioHang>();
                Session["GioHang"] = lstgiohang;
                return lstgiohang;
            }
            return lstgiohang;
        }
        //Them Gio Hang
        public ActionResult ThemGioHang(int MaSP, string url)
        {
            //Kiem tra san pham co ton tai hay khong
            SanPham sp = db.SanPham.SingleOrDefault(x => x.MaSP == MaSP);
            if (sp == null)
            {
                //trỏ ddeen duong dan khong hop le
                Response.StatusCode = 404;
                return null;
            }
            //Lay giỏ hàng
            List<ItemGioHang> lst = LayGioHang();
            //TH1:Sp da tồn tai trong giỏ hàng
            ItemGioHang spcheck = lst.SingleOrDefault(x => x.MaSP == MaSP);
            if (spcheck != null)
            {
                if (sp.SoLuongTon < spcheck.SoLuong)
                {
                    return View("ThongBao");
                }
                spcheck.SoLuong++;
                spcheck.ThanhTien = (decimal)(spcheck.SoLuong * spcheck.DonGia);
                return Redirect(url);
            }
            //Kiem tra so lượng hàng tồn với hàng đặt

            //chua tồn tại =>thêm  1 sp mới
            ItemGioHang itemGH = new ItemGioHang(MaSP);
            if (sp.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            lst.Add(itemGH);
            return Redirect(url);
        }
        //Tính tổng số lượng
        public double TongSL()
        {
            List<ItemGioHang> lst = (List<ItemGioHang>)Session["GioHang"];
            if (lst != null)
            {
                return lst.Sum(x => x.SoLuong);
            }
            return 0;
        }
        //Tinh tông tiền
        public decimal TongTien()
        {
            List<ItemGioHang> lst = (List<ItemGioHang>)Session["GioHang"];
            if (lst != null)
            {
                return lst.Sum(x => x.ThanhTien);
            }
            return 0;
        }
        public ActionResult XemGioHang()
        {
            List<int> list = new List<int>();
            list.Add(1); list.Add(2); list.Add(3); list.Add(4); list.Add(5); list.Add(6); list.Add(7); list.Add(8); list.Add(9); list.Add(10);
            ViewBag.SoLuong = new SelectList(list);
            //Lay giỏ Hàng show ra
            List<ItemGioHang> lst = LayGioHang();
            return View(lst);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSL = TongSL();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //Chinh sua gio hang
        public ActionResult UpdateGioHang(int? MaSP)
        {
            //Kiem tra sessetion co tôn tai hay không
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "ControlHome");
            }
            //Kiêm tra măt hang co tôn tại trong csdl k
            SanPham sp = db.SanPham.SingleOrDefault(x => x.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lay list gio hang tu session
            List<ItemGioHang> lst = Session["GioHang"] as List<ItemGioHang>;
            //Kiem tra sp co tôn tại trong session k
            ItemGioHang item = lst.SingleOrDefault(n => n.MaSP == MaSP);
            if (item == null)
            {
                return RedirectToAction("Index", "ControlHome");

            }
            ViewBag.lstgiohang = lst;
            return View(item);
        }
        //Xử lú cập nhật
        [HttpPost]
        public ActionResult CapnhatGH(ItemGioHang itemGH)
        {
            //kiểm tra số lượng tồn
            SanPham spCheck = db.SanPham.Single(x => x.MaSP == itemGH.MaSP);
            if (spCheck.SoLuongTon < itemGH.SoLuong)
            {
                return View("Thông báo");
            }
            List<ItemGioHang> lst = LayGioHang();
            ItemGioHang item = lst.Find(x => x.MaSP == itemGH.MaSP);
            item.SoLuong = itemGH.SoLuong;
            item.ThanhTien = (decimal)item.DonGia * item.SoLuong;
            TongTien();
            return RedirectToAction("XemGioHang");
        }
        //xoa gio hang
        public ActionResult XoaGH(int MaSP)
        {
            //Kiem tra sessetion co tôn tai hay không
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "ControlHome");
            }
            //Kiêm tra măt hang co tôn tại trong csdl k
            SanPham sp = db.SanPham.SingleOrDefault(x => x.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lay ;list gio hang
            List<ItemGioHang> lstgiohang = LayGioHang();
            //Kiem tra sp co tôn tại trong session k
            ItemGioHang item = lstgiohang.SingleOrDefault(n => n.MaSP == MaSP);
            if (item == null)
            {
                return RedirectToAction("Index", "ControlHome");

            }
            lstgiohang.Remove(item);
            return RedirectToAction("XemGioHang");
        }
        //Đặt Hàng
        [HttpPost]
        public ActionResult DatHang(KhachHang kh)
        {
            //Kiem tra sessetion co tôn tai hay không
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "ControlHome");
            }
            KhachHang khachhang=new KhachHang();
            //kiêm tra loại khách hàng có tai khoan chưa
            if (Session["TaiKhoan"] == null)//kh chưa co TK
            {
                khachhang = kh;
                db.KhachHang.Add(khachhang);
                db.SaveChanges();
            }
            else //kh la thành viên (có tài khoản)
            {
                ThanhVien tv = Session["TaiKhoan"] as ThanhVien;
                khachhang.TenKH = tv.HoTen;
                khachhang.DiaChi = tv.DiaChi;
                khachhang.Email = tv.Email;
                khachhang.SoDienThoai = tv.SoDienThoai;
                khachhang.MaThanhVien = tv.MaTV;
                db.KhachHang.Add(khachhang);
                db.SaveChanges();
            }
            //thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            ddh.MaKH = khachhang.MaKH;
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            ddh.UuDai = 0;
            ddh.DaHuy = false;
            ddh.DaXoa = false;
            db.DonDatHang.Add(ddh);
            db.SaveChanges();
            //thêm chi tiêt đơn hàng
            List<ItemGioHang> lst = LayGioHang();
            foreach(var item in lst)
            {
                ChiTietDonDatHang ctdh = new ChiTietDonDatHang();
                ctdh.MaDDH = ddh.MaDDH;
                ctdh.MaSP = item.MaSP;
                ctdh.TenSP = item.TenSP;
                ctdh.SoLuong = item.SoLuong;
                ctdh.DonGia = (decimal)item.DonGia;
                db.ChiTietDonDatHang.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang");
        }
        //Them Gio Hang Ajax
        public ActionResult ThemGioHangAjax(int MaSP, string url)
        {
            //Kiem tra san pham co ton tai hay khong
            SanPham sp = db.SanPham.SingleOrDefault(x => x.MaSP == MaSP);
            if (sp == null)
            {
                //trỏ ddeen duong dan khong hop le
                Response.StatusCode = 404;
                return null;
            }
            //Lay giỏ hàng
            List<ItemGioHang> lst = LayGioHang();
            //TH1:Sp da tồn tai trong giỏ hàng
            ItemGioHang spcheck = lst.SingleOrDefault(x => x.MaSP == MaSP);
            if (spcheck != null)
            {
                if (sp.SoLuongTon < spcheck.SoLuong)
                {
                    return Content("<script> alert(\"Hết Hàng\");</script>");
                }
                spcheck.SoLuong++;
                spcheck.ThanhTien = (decimal)(spcheck.SoLuong * spcheck.DonGia);
                ViewBag.TongTien = TongTien();
                ViewBag.TongSl = TongSL();
                return PartialView("GioHangPartial");
            }
            //Kiem tra so lượng hàng tồn với hàng đặt

            //chua tồn tại =>thêm  1 sp mới
            ItemGioHang itemGH = new ItemGioHang(MaSP);
            if (sp.SoLuongTon < itemGH.SoLuong)
            {
                return Content("<script> alert(\"Hết Hàng\");</script>");
            }
            lst.Add(itemGH);
            ViewBag.TongTien = TongTien();
            ViewBag.TongSl = TongSL();
            return PartialView("GioHangPartial");
        }

    }
}