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
            // Check if user exists with the provided credentials
            var nguoiDung = await _context.NguoiDungs
                .FirstOrDefaultAsync(u => u.TenDangNhap == tenDangNhap && u.MatKhau == matKhau);

            if (nguoiDung == null)
            {
                TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View("Index");
            }
            else
            {
                return RedirectToAction("Index", "SinhViens");
            }
        }
    }
}
