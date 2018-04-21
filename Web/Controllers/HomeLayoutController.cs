using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using System.Web.Security;

namespace Web.Controllers
{

    public class HomeLayoutController : Controller
    {
        // GET: HomeLayout
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult Index()
        {
            //lan luot tao cac viewbag de lay list SP tu CSDL
            //dien thoai moi nhat
            var listdienthoai = db.SanPham.Where(x=>x.MaLoaiSP==2&&x.Moi==1 &&x.DaXoa==false);
            ViewBag.lstDT = listdienthoai;
            var listmtb= db.SanPham.Where(x => x.MaLoaiSP == 3 && x.Moi == 1 && x.DaXoa == false);
            ViewBag.lstmtb = listmtb;
            var listlaptop= db.SanPham.Where(x => x.MaLoaiSP==1&&x.Moi == 1 && x.DaXoa == false);
            ViewBag.lstLT = listlaptop;
            return View();
        }
        public ActionResult MenuPartial()
        {
            var listSP = db.SanPham;
            return PartialView(listSP);
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            ViewBag.CauHoi = new SelectList( Cauhoibimat());
            return View();
        }
        [HttpGet]
        public ActionResult DangKy1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy1(ThanhVien tv)
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy( ThanhVien tv)
        {
            //Kiem tra captcha hop le
            if(this.IsCaptchaValid(""))
            {
                if (ModelState.IsValid)
                {
                    //Add 1 Thanh vien
                    ViewBag.ThongBao = "Them Thanh Cong";
                    db.ThanhVien.Add(tv);
                    db.SaveChanges();
                }
                else ViewBag.ThongBao = "Thêm Thất Bại";
                return View();
            }
            ViewBag.ThongBao = "Sai Ma Captcha";
            return View();

        }
        public List<string> Cauhoibimat()
        {
            List<string> list = new List<string>();
            list.Add("Con vật bạn yêu thích nhất là gì");
            list.Add("Người bạn yêu quý nhất là ai");
            list.Add("Bạn là nam hay nữ");
            return list;
        }
        public ActionResult DangNhap( FormCollection f)
        {
            // Kiem tra dang nhap
            string taikhoan = f["txtname"].ToString();
            string pass = f["txtpass"].ToString();
            ThanhVien tv = db.ThanhVien.SingleOrDefault(x => x.TaiKhoan == taikhoan && x.MatKhau == pass);
            if(tv!=null)
            {
                Session["TaiKhoan"] = tv;
                return RedirectToAction("Index");
            }
            return Content("Tai Khoan Khong Ton Tai");
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string txtTaiKhoan= f["txtname"].ToString();
            string txtMatKhau = f["txtpass"].ToString();
            var checktaikhoan = db.ThanhVien.Where(x => x.TaiKhoan == txtTaiKhoan);
            var checkMatKhau = db.ThanhVien.Where(x => x.MatKhau == txtMatKhau);
            if (checktaikhoan == null)
            {
                return Content("Tài Khoản Không Tồn Tại");
            }
            else if(checkMatKhau == null)
            {
                return Content("Mật Khẩu Sai");
            }

            ThanhVien tv = db.ThanhVien.SingleOrDefault(n => n.TaiKhoan == txtTaiKhoan && n.MatKhau == txtMatKhau);
            if (tv != null)
            {
                //Truy cập lấy ra tất cả quyền của thành viên đó
                var lstQuyen = db.LoaiThanhVien_Quyen.Where(n => n.MaLoaiTV == tv.MaLoaiTV);
                string Quyen = "";
                //Duyệt list quyền
                foreach (var item in lstQuyen)
                {
                    Quyen += item.Quyen.TenQuyen + ",";
                    //Lấy quyền trong bảng chi tiết quyền và loại thành viên“DangKy,QuanLyDonHang,QuanLySanPham,”
                }
                Quyen = Quyen.Substring(0, Quyen.Length - 1);
                //Cắt đi dấu , cuối cùng (Chuỗi sau khi cắt: “DangKy,QuanLyDonHang,QuanLySanPham”
                PhanQuyen(txtTaiKhoan, Quyen);
                //Xử lý phương thức phân quyền
                Session["TaiKhoan"] = tv;
                return RedirectToAction("Index", "HomeLayout");
            }
            return Content("Đăng Nhập Thất Bại!! Xin Hãy Thử Lại");

        }

        private void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                                                        TaiKhoan, //user
                                                        DateTime.Now, //begin
                                                        DateTime.Now.AddHours(1), //timeout
                                                        false, //remember?
                                                        Quyen, // permission.. "admin" or for more than one            "admin,marketing,sales"
                                                        FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                        FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent)
                cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);

        }

        public ActionResult DemoJquery()
        {
            return View();
        }
        //tạo trang chăn quyền truy cập
        public ActionResult PreventAccess()
        {
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        public ActionResult MenuProductLeftPartial()
        {
            var listSP1 = db.SanPham;
            ViewBag.lstSP1 = listSP1;
            return PartialView(listSP1);
        }

    }
}