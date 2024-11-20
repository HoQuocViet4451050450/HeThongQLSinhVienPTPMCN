using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPSTUDENT.Models
{
    public class NhomTinNhan
    {
        public int MaNhom { get; set; }

        [Required(ErrorMessage = "Chủ đề là bắt buộc")]
        public string ChuDe { get; set; }

        [Required(ErrorMessage = "Người tạo ID là bắt buộc")]
        public int NguoiTaoId { get; set; }

        // Quan hệ với NguoiDung
        public NguoiDung NguoiTao { get; set; }

        // Liên kết với các thành viên trong nhóm
        public ICollection<ThanhVienNhom> ThanhVienNhoms { get; set; }

        // Liên kết với các tin nhắn trong nhóm
        public ICollection<TinNhan> TinNhans { get; set; }
    }
}
