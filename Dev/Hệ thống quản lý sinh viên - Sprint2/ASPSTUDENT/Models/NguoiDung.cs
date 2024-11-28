using System.ComponentModel.DataAnnotations;

namespace ASPSTUDENT.Models
{
    public class NguoiDung
    {
        public int MaNguoiDung { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 đến 100 ký tự")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Loại người dùng là bắt buộc")]
        public string LoaiNguoiDung { get; set; }

        // Liên kết với các nhóm tin nhắn mà người dùng tham gia
        public ICollection<ThanhVienNhom> NhomThamGia { get; set; } = new List<ThanhVienNhom>();

        // Liên kết với các tin nhắn gửi đi
        public ICollection<TinNhan> TinNhans { get; set; } = new List<TinNhan>();
    }


}
