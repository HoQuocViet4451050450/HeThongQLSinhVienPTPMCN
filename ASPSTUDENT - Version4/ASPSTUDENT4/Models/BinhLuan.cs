using System.ComponentModel.DataAnnotations;

namespace ASPSTUDENT4.Models
{
    public class BinhLuan
    {
        [Key]
        public int MaBinhLuan { get; set; }
        public int MaBaiDang { get; set; }
        public int MaNguoiDung { get; set; }
        public string NoiDung { get; set; }
        public string DuongDanAnh { get; set; }
        public DateTime? NgayTao { get; set; }

        // Navigation properties
        public BaiDang BaiDang { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }
}
