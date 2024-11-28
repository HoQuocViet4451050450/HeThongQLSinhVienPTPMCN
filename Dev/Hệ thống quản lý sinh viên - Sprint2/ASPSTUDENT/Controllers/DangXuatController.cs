using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ASPSTUDENT.Controllers
{
    public class DangXuatController : Controller
    {
        // Phương thức đăng xuất
        public IActionResult Index()
        {
            // Xóa thông tin người dùng khỏi session
            HttpContext.Session.Clear();

            // Điều hướng về trang đăng nhập
            return RedirectToAction("Index", "DangNhaps");
        }
    }
}
