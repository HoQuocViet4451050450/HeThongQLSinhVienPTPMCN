using System.ComponentModel.DataAnnotations;

namespace ASPSTUDENT4.Models
{
    public class NguoiDung
    {
        [Key]
        public int MaNguoiDung { get; set; }
        public string HoTen { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string LoaiNguoiDung { get; set; }
        public DateTime? NgayTao { get; set; }

        // Navigation property
        public ICollection<BaiDang> BaiDangs { get; set; }
        public ICollection<BinhLuan> BinhLuans { get; set; }
        public ChiTietSinhVien ChiTietSinhVien { get; set; }
    }
}
