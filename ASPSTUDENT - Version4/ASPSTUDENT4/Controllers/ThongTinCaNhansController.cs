using ASPSTUDENT4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPSTUDENT4.Controllers
{
    public class ThongTinCaNhansController : Controller
    {
        private readonly ASPSTUDENTContext _context;

        public ThongTinCaNhansController(ASPSTUDENTContext context)
        {
            _context = context;
        }
        // GET: ThongTinCaNhan
        public async Task<IActionResult> Index()
        {
            // Lấy MaNguoiDung từ session
            var userIdString = HttpContext.Session.GetString("MaNguoiDung");

            if (string.IsNullOrEmpty(userIdString))
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập trước.";
                return RedirectToAction("Index", "DangNhaps");
            }

            // Chuyển đổi từ string sang int
            var userId = int.Parse(userIdString);

            // Tìm người dùng theo MaNguoiDung
            var nguoiDung = await _context.NguoiDungs
                                          .FirstOrDefaultAsync(n => n.MaNguoiDung == userId);

            // Kiểm tra xem người dùng có tồn tại không
            if (nguoiDung == null)
            {
                TempData["ErrorMessage"] = "Người dùng không tồn tại.";
                return RedirectToAction("Index", "DangNhaps");
            }

            // Trả về view với thông tin người dùng
            return View(nguoiDung);
        }
    }
}
