using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ItemGioHang
    {
       public string TenSP { set; get; }
       public int MaSP { set; get; }
       public int SoLuong { set; get; }
       public double DonGia { set; get; }
       public decimal ThanhTien { set; get; }
       public string HinhAnh { set; get; }
        public ItemGioHang(int imasp )
        {
            using (QuanLyBanHangEntities db=new QuanLyBanHangEntities())
            {
                this.MaSP = imasp;
                SanPham sp = db.SanPham.Single(x => x.MaSP==imasp);
                this.TenSP = sp.TenSP;
                this.DonGia = sp.DonGia.Value;
                this.HinhAnh = sp.HinhAnh;
                this.ThanhTien =(decimal) DonGia * SoLuong;

            }
        }
        public ItemGioHang(int imasp,int sl)
        {
            using (QuanLyBanHangEntities db = new QuanLyBanHangEntities())
            {
                this.MaSP = imasp;
                SanPham sp = db.SanPham.Single(x => x.MaSP == imasp);
                this.TenSP = sp.TenSP;
                this.DonGia = sp.DonGia.Value;
                this.HinhAnh = sp.HinhAnh;
                this.ThanhTien = (decimal)DonGia * sl;

            }
        }
        public ItemGioHang() { }
    }
}