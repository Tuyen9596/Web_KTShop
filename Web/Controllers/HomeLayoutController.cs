using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using System.Web.Security;
using BotDetect.Web.Mvc;
using Facebook;
using System.Configuration;

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
            var listdienthoai = db.SanPham.Where(x=>x.MaLoaiSP==2&&x.Moi==1 &&x.DaXoa==false).ToList();
            ViewBag.lstDT = listdienthoai;
            var listmtb= db.SanPham.Where(x => x.MaLoaiSP == 3 && x.Moi == 1 && x.DaXoa == false).ToList();
            ViewBag.lstmtb = listmtb;
            var listlaptop= db.SanPham.Where(x => x.MaLoaiSP==1&&x.Moi == 1 && x.DaXoa == false).ToList();
            ViewBag.lstLT = listlaptop;
            return View();
        }
        [ChildActionOnly]
        public ActionResult MenuPartial()
        {
            var listSP = db.SanPham.ToList();
            return PartialView(listSP);
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            ViewBag.CauHoi = new SelectList( Cauhoibimat());
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [CaptchaValidation("CaptchaCode", "exampleCaptcha", "Mã xác nhận không đúng!")]
        public ActionResult DangKy(ThanhVien tv)
        {
            //Kiem tra captcha hop le
            if(ModelState.IsValid)
            {
                //Add 1 Thanh vien
                tv.MaLoaiTV = 2;
                db.ThanhVien.Add(tv);
                db.SaveChanges();
                ViewBag.ThongBao = "Thêm Thành Công";

            }else

            {
                ViewBag.loi = "Thêm Thất Bại";
                return View();
            }
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
        public bool checkUser(string user)
        {
            if (db.ThanhVien.Where(x => x.TaiKhoan == user).Count() > 0)
                return true;
            return false;
        }
        public ActionResult LoginFaceBook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["AppID"],
                client_secret = ConfigurationManager.AppSettings["AppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return View();
        }
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string msg="fail";
            string txtTaiKhoan= f["txtname"].ToString();
            string txtMatKhau = f["txtpass"].ToString();
            var checktaikhoan = db.ThanhVien.Where(x => x.TaiKhoan == txtTaiKhoan).ToList();
            var checkMatKhau = db.ThanhVien.Where(x => x.MatKhau == txtMatKhau).ToList();
            if (checktaikhoan.Count == 0)
            {
                msg = "saitk";
            }
            else if (checkMatKhau.Count == 0)
            {
                msg = "saimk";
            }
            else msg = "success";

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
            }
            return Content(msg,"text/plain");

        }
        //public JsonResult CheckValidUser(SiteUser model)
        //{
        //    string result = "Fail";
        //    var DataItem = db.SiteUsers.Where(x => x.Email == model.Email && x.Password == model.Password).SingleOrDefault();
        //    if (DataItem != null)
        //    {
        //        Session["UserID"] = DataItem.ID.ToString();
        //        Session["UserName"] = DataItem.Username.ToString();
        //        result = "Success";
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

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
        public ActionResult DangXuat(string url)
        {
            Session["TaiKhoan"] = null;
            FormsAuthentication.SignOut();
            return Redirect(url);
        }
        public ActionResult MenuProductLeftPartial()
        {
            var listSP1 = db.SanPham.ToList();
            ViewBag.lstSP1 = listSP1;
            return PartialView(listSP1);
        }

    }
}