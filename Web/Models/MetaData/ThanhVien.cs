using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XayDungWeb.Models
{
    [MetadataTypeAttribute(typeof(ThanhVienMetadata))]
    public partial class ThanhVien
    {
        internal sealed class ThanhVienMetadata
        {//Danh sách các thuộc tính}
            [DisplayName("Mã Thành Viên")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [Range(1, 5, ErrorMessage = "{0} phảitừ{1} đến{2}")]
            public int MaTV { get; set; }
            [DisplayName("Tài Khoản")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [StringLength(10, ErrorMessage = "Không quá 10 kýtự")]
            public string TaiKhoan { get; set; }
            [DisplayName("Mật Khẩu")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string MatKhau { get; set; }
            [Required(ErrorMessage = "{0} không được để trống")]
            [DisplayName("Họ Tên")]
            public string HoTen { get; set; }
            [DisplayName("Địa Chỉ")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string DiaChi { get; set; }
            [DisplayName("Email")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Email khônghợp lệ!")]
            public string Email { get; set; }
            [DisplayName("Số Điên thoại")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string SoDienThoai { get; set; }
            [DisplayName("Câu Hỏi")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string CauHoi { get; set; }
            [DisplayName("Câu Trả Lời")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string CauTraLoi { get; set; }
            [DisplayName("Mã Loại Thành Viên")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public Nullable<int> MaLoaiTV { get; set; }


        }
    }
}