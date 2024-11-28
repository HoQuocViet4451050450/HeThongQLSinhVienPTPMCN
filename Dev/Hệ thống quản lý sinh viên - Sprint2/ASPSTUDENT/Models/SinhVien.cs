using ASPSTUDENT.Models;
using System.ComponentModel.DataAnnotations;

public class SinhVien
{
    [Required(ErrorMessage = "Mã sinh viên là bắt buộc")]
    public string MaSinhVien { get; set; }

    [Required(ErrorMessage = "Họ tên là bắt buộc")]
    public string HoTen { get; set; }

    [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
    public DateTime NgaySinh { get; set; }

    [Required(ErrorMessage = "Giới tính là bắt buộc")]
    public string GioiTinh { get; set; }

    [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
    public string SoDienThoai { get; set; }

    [Required(ErrorMessage = "Mã lớp là bắt buộc")]
    public string MaLop { get; set; }

    public int TongTinChi { get; set; } = 0;
    public decimal GPA { get; set; } = 0.0m;
    public string TrangThai { get; set; } = "Chưa xác định";
    public string QueQuan { get; set; } = "Chưa có thông tin";

    // Khóa ngoại đến LopHoc
    public LopHoc LopHoc { get; set; }
}

