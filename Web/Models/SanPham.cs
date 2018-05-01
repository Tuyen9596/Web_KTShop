//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.BinhLuan = new HashSet<BinhLuan>();
            this.ChiTietDonDatHang = new HashSet<ChiTietDonDatHang>();
            this.ChiTietPhieunhap = new HashSet<ChiTietPhieunhap>();
        }
    
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public Nullable<double> DonGia { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public string CauHinh { get; set; }
        public string TomTat { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<int> SoLuongTon { get; set; }
        public Nullable<int> LuotXem { get; set; }
        public Nullable<int> LuotBinhChon { get; set; }
        public Nullable<int> LuotBinhLuan { get; set; }
        public Nullable<int> SoLanMua { get; set; }
        public Nullable<int> Moi { get; set; }
        public Nullable<int> MaNCC { get; set; }
        public Nullable<int> MaNSX { get; set; }
        public Nullable<int> MaLoaiSP { get; set; }
        public Nullable<bool> DaXoa { get; set; }
        public string Hinhanh2 { get; set; }
        public string Hinhanh3 { get; set; }
        public string Hinhanh4 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHang { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieunhap> ChiTietPhieunhap { get; set; }
        public virtual LoaiSanPham LoaiSanPham { get; set; }
        public virtual NhaCC NhaCC { get; set; }
        public virtual NhaSanXuat NhaSanXuat { get; set; }
    }
}
