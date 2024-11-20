using System.ComponentModel.DataAnnotations;

namespace ASPSTUDENT.Models
{
    public class LopHoc
    {
        [Required(ErrorMessage = "Mã lớp là bắt buộc")]
        public string MaLop { get; set; }  // Đây là khóa chính của bảng LopHoc

        [Required(ErrorMessage = "Tên lớp là bắt buộc")]
        public string TenLop { get; set; }

        // Quan hệ với SinhVien
        public ICollection<SinhVien> SinhViens { get; set; } = new List<SinhVien>();
    }
}
