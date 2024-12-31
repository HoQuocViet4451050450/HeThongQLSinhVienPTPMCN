using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ASPSTUDENT4.Models
{
    public class Student
    {
        [Key]
        public int MaSV { get; set; }  // Mã sinh viên
        public string HoTen { get; set; }  // Họ tên sinh viên
        public string TenDangNhap { get; set; }  // Tên đăng nhập
        public string MatKhau { get; set; }  // Mật khẩu (nên mã hóa mật khẩu trong thực tế)
        public int MaLop { get; set; }  // Mã lớp học mà sinh viên thuộc về
        public Class LopHoc { get; set; }

        // Liên kết với lớp học (Class)
        public virtual Class Class { get; set; }
    }
}
