using Microsoft.AspNetCore.Mvc;

namespace ASPSTUDENT4.Controllers
{
    public class DangXuatsController : Controller
    {
        public IActionResult Index()
        {
            // Xóa thông tin người dùng khỏi session
            HttpContext.Session.Clear();

            // Điều hướng về trang đăng nhập
            return RedirectToAction("Index", "DangNhaps");
        }
    }
}
