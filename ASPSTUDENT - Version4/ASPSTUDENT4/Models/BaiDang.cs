using System.ComponentModel.DataAnnotations;

namespace ASPSTUDENT4.Models
{
    public class BaiDang
    {
        [Key] // Add this to explicitly define the primary key
        public int MaBaiDang { get; set; }
        public int MaNguoiDung { get; set; }
        public string NoiDung { get; set; }
        public string DuongDanAnh { get; set; }
        public DateTime? NgayTao { get; set; }

        // Navigation property
        public NguoiDung NguoiDung { get; set; }
        public ICollection<BinhLuan> BinhLuans { get; set; }
    }
}
