using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ASPSTUDENT.Controllers
{
    public class DangXuatController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Đăng xuất khỏi hệ thống
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Điều hướng về trang chủ (hoặc trang đăng nhập)
            return RedirectToAction("Index", "Home");
        }
    }
}
