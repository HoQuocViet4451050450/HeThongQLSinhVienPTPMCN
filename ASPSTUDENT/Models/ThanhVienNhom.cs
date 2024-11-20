using System;
using System.ComponentModel.DataAnnotations;

namespace ASPSTUDENT.Models
{
    public class ThanhVienNhom
    {
        [Required(ErrorMessage = "Mã nhóm là bắt buộc")]
        public int MaNhom { get; set; }

        public NhomTinNhan NhomTinNhan { get; set; }

        [Required(ErrorMessage = "Mã người dùng là bắt buộc")]
        public int MaNguoiDung { get; set; }

        public NguoiDung NguoiDung { get; set; }

        [Required(ErrorMessage = "Ngày tham gia là bắt buộc")]
        public DateTime NgayThamGia { get; set; }
    }
}
