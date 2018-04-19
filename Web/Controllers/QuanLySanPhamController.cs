using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web;
using System.Net.Mail; //using thư viện gửi mail
using System.Web.Mvc;
using Web.Models;
namespace Web.Controllers
{
    [Authorize(Roles = "admin,Mod,Member")]
    public class QuanLySanPhamController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: QuanLySanPham
        [Authorize(Roles = "Member")]
        public ActionResult Index()
        {
            return View(db.SanPham.Where(n=>n.DaXoa==false));
        }
        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
            //lay sp can chi nh sua
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPham.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaNCC = new SelectList(db.NhaCC.OrderBy(x => x.MaNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPham.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuat.OrderBy(x => x.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);

            return View(sp);
        }
        [ValidateInput(false)]
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult ChinhSua(SanPham model)
        {
            //bỏ ua phân kiêm tra .sưa trưc tiêp
            ViewBag.MaNCC = new SelectList(db.NhaCC.OrderBy(x => x.MaNCC), "MaNCC", "TenNCC", model.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPham.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai", model.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuat.OrderBy(x => x.MaNSX), "MaNSX", "TenNSX", model.MaNSX);
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
             db.SaveChanges();
                return RedirectToAction("Index");
          
        }
        public ActionResult Xoa(int id)//DaXOa=rue
        {
            //lay sp can chi nh sua
            if (id ==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPham.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaNCC = new SelectList(db.NhaCC.OrderBy(x => x.MaNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPham.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuat.OrderBy(x => x.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);
            sp.DaXoa = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ChuaThanhToan()
        {
            var lst = db.DonDatHang.Where(n => n.DaThanhToan == false).OrderBy(n=>n.NgayDat);
            return View(lst);
        }
        public ActionResult ChuaGiao()
        {
            var lst = db.DonDatHang.Where(n => n.TinhTrangGiaoHang == false&&n.DaThanhToan==true).OrderBy(n => n.NgayDat);
            return View(lst);
        }
        public ActionResult DaGiaoDaThanhToan()
        {
            var lst = db.DonDatHang.Where(n => n.TinhTrangGiaoHang == true && n.DaThanhToan==true).OrderBy(n => n.NgayDat);
            return View(lst);
        }
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult DuyetDonHang(int? id)
        {
            //Kiem tra id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DonDatHang model = db.DonDatHang.SingleOrDefault(n => n.MaDDH == id);
            //Kiem tra don hang co ton tai hay k
            if (model == null)
            {
                return HttpNotFound();
            }
            //lay danh sach chi tiet don dat hang de hien thi
            var lstChiTietDDH = db.ChiTietDonDatHang.Where(n => n.MaDDH == id);
            ViewBag.ListChiTietDDh = lstChiTietDDH;
            return View(model);
        }
        [HttpPost]
            public ActionResult DuyetDonHang (DonDatHang ddh){
            DonDatHang ddhupdate = db.DonDatHang.SingleOrDefault(n=>n.MaDDH==ddh.MaDDH);
            ddhupdate.DaThanhToan = ddh.DaThanhToan;
            ddhupdate.TinhTrangGiaoHang = ddh.TinhTrangGiaoHang;
            db.SaveChanges();
            var lstChiTietDDH = db.ChiTietDonDatHang.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.ListChiTietDDh = lstChiTietDDH;
            //Gui Mail
            GuiEmail("Xac Nhan Don Hang","tuxenpham18@gmail.com","tuxenpham@gmail.com","Kissofdeath96","Thanh COng");
            return View(ddhupdate);

        }
            public void GuiEmail(string Title, string ToEmail, string FromEmail, string PassWord, string Content)
        {
            // goi email
            MailMessage mail = new MailMessage();mail.To.Add(ToEmail); // Địa chỉ nhận
            mail.From = new MailAddress(ToEmail); // Địa chửi gửi
            mail.Subject = Title; // tiêu đề gửi
            mail.Body = Content; // Nội dung
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; // host gửi của Gmail
            smtp.Port = 587; //port của Gmail
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(FromEmail, PassWord);//Tài khoản password người gửi
            smtp.EnableSsl = true; //kích hoạt giao tiếp an toàn SSL
            smtp.Send(mail); //Gửi mail đi
        }

    }
}