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
    
    public partial class PhieuNhap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuNhap()
        {
            this.ChiTietPhieunhap = new HashSet<ChiTietPhieunhap>();
        }
    
        public int MaPN { get; set; }
        public int MaNCC { get; set; }
        public System.DateTime NgayNhap { get; set; }
        public Nullable<bool> DaXoa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieunhap> ChiTietPhieunhap { get; set; }
        public virtual NhaCC NhaCC { get; set; }
    }
}
