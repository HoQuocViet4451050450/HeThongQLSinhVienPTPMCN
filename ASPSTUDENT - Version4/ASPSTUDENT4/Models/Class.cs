using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace ASPSTUDENT4.Models
{
    public class Class
    {
        [Key]  // Đánh dấu MaLop là khóa chính
        public int MaLop { get; set; }
        public string TenLop { get; set; }

    }
}