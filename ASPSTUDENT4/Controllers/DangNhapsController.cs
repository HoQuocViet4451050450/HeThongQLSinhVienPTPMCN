using ASPSTUDENT4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPSTUDENT4.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ASPSTUDENT4.Controllers
{
    public class DangNhapsController : Controller
    {
        private readonly ASPSTUDENTContext _context;

        // Constructor to inject the DbContext
        public DangNhapsController(ASPSTUDENTContext context)
        {
            _context = context;
        }

        // GET: Index page
        public IActionResult Index()
        {
            return View();
        }

        // POST: Login action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                TempData["ErrorMessage"] = "Tên đăng nhập và mật khẩu không thể trống.";
                return View("Index");
            }

            // Find the user by username
            var nguoiDung = await _context.NguoiDungs
                                          .FirstOrDefaultAsync(u => u.TenDangNhap == tenDangNhap);

            // Check if the user exists
            if (nguoiDung == null || nguoiDung.MatKhau != matKhau) // In production, use hashed password comparison
            {
                TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View("Index");
            }

            // Lưu thông tin người dùng vào session
            HttpContext.Session.SetString("MaNguoiDung", nguoiDung.MaNguoiDung.ToString());
            HttpContext.Session.SetString("LoaiNguoiDung", nguoiDung.LoaiNguoiDung);
            // Redirect to Home page if login successful
            return RedirectToAction("Index", "ChiTietSinhViens");
        }
    }
}
