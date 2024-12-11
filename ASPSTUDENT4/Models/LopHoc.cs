using System.ComponentModel.DataAnnotations;

namespace ASPSTUDENT4.Models
{
    public class LopHoc
    {
        [Key]
        public int MaLop { get; set; }
        public string TenLop { get; set; }

        // Navigation property
        public ICollection<ChiTietSinhVien> ChiTietSinhViens { get; set; }
    }
}
