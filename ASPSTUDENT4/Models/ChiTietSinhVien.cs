using System.ComponentModel.DataAnnotations;

namespace ASPSTUDENT4.Models
{
    public class ChiTietSinhVien
    {
        [Key]
        public int MaSinhVien { get; set; }
        public int MaLop { get; set; }

        public string QueQuan { get; set; }
        public string SoDienThoai { get; set; }
        public int TongTinChi { get; set; }
        public decimal DiemGPA { get; set; }

        // Navigation properties
        public NguoiDung NguoiDung { get; set; }
        public LopHoc LopHoc { get; set; }
    }
}
