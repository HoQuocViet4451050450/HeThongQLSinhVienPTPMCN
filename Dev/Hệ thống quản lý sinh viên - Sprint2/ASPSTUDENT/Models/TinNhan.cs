using System;
using System.ComponentModel.DataAnnotations;

namespace ASPSTUDENT.Models
{
    public class TinNhan
    {
        public int MaTinNhan { get; set; }

        [Required(ErrorMessage = "Mã nhóm là bắt buộc")]
        public int MaNhom { get; set; }

        public NhomTinNhan NhomTinNhan { get; set; }

        [Required(ErrorMessage = "Mã người gửi là bắt buộc")]
        public int NguoiGuiId { get; set; }

        public NguoiDung NguoiGui { get; set; }

        [Required(ErrorMessage = "Nội dung tin nhắn là bắt buộc")]
        public string NoiDung { get; set; }

        [Required(ErrorMessage = "Thời gian gửi là bắt buộc")]
        public DateTime ThoiGianGui { get; set; }

        [Required(ErrorMessage = "Loại tin nhắn là bắt buộc")]
        public string LoaiTinNhan { get; set; }

        public string DuongDanTapTin { get; set; }
    }
}
