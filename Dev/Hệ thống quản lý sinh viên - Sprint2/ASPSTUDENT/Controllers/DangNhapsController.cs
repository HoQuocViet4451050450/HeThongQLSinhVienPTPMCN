using ASPSTUDENT.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPSTUDENT.Controllers
{
    public class DangNhapsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DangNhapsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string tenDangNhap, string matKhau)
        {
            var nguoiDung = await _context.NguoiDungs
                .FirstOrDefaultAsync(u => u.TenDangNhap == tenDangNhap && u.MatKhau == matKhau);

            if (nguoiDung == null)
            {
                TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View("Index");
            }

            // Lưu thông tin người dùng vào session
            HttpContext.Session.SetString("LoaiNguoiDung", nguoiDung.LoaiNguoiDung);

            return RedirectToAction("Index", "SinhViens");
        }

    }
}
