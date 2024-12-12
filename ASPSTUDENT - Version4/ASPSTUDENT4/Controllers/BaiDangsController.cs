using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPSTUDENT4.Data;
using ASPSTUDENT4.Models;

namespace ASPSTUDENT4.Controllers
{
    public class BaiDangsController : Controller
    {
        private readonly ASPSTUDENTContext _context;

        public BaiDangsController(ASPSTUDENTContext context)
        {
            _context = context;
        }

        // GET: BaiDangs
        public async Task<IActionResult> Index(string searchString)
        {
            // Lấy danh sách bài đăng với dữ liệu người đăng
            var baiDangsQuery = _context.BaiDangs.Include(b => b.NguoiDung).AsQueryable();

            // Kiểm tra nếu có từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                baiDangsQuery = baiDangsQuery.Where(b => b.NguoiDung.HoTen.Contains(searchString));
                ViewData["SearchString"] = searchString; // Lưu từ khóa tìm kiếm để giữ trong ô tìm kiếm
            }

            var baiDangs = await baiDangsQuery.ToListAsync();
            return View(baiDangs);
        }

        // GET: BaiDangs/Create
        public IActionResult Create()
        {
            // Hiển thị danh sách người dùng cho việc tạo bài đăng
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDungs, "MaNguoiDung", "HoTen");
            return View();
        }

        // POST: BaiDangs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBaiDang, NoiDung, NgayTao")] BaiDang baiDang)
        {
            // Nếu không có ảnh, có thể gán giá trị mặc định
            baiDang.DuongDanAnh = ""; // Đã bỏ trường DuongDanAnh khỏi Model

            // Lấy MaNguoiDung từ session
            var userIdString = HttpContext.Session.GetString("MaNguoiDung");

            if (userIdString == null)
            {
                // Nếu không có MaNguoiDung trong session, chuyển hướng về trang đăng nhập hoặc thông báo lỗi
                return RedirectToAction("Index", "DangNhaps");
            }

            if (int.TryParse(userIdString, out int userId))
            {
                baiDang.MaNguoiDung = userId;  // Gán MaNguoiDung cho bài đăng
            }
            else
            {
                return RedirectToAction("Index", "DangNhaps");
            }

            _context.Add(baiDang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: BaiDangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiDang = await _context.BaiDangs
                .Include(b => b.NguoiDung)
                .FirstOrDefaultAsync(m => m.MaBaiDang == id);

            if (baiDang == null)
            {
                return NotFound();
            }

            return View(baiDang);
        }

        // POST: BaiDangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Lấy bài đăng cần xóa
            var baiDang = await _context.BaiDangs.FindAsync(id);

            if (baiDang != null)
            {
                // Xóa các bình luận liên quan đến bài đăng này (nếu có)
                var binhLuans = _context.BinhLuans.Where(b => b.MaBaiDang == id);
                _context.BinhLuans.RemoveRange(binhLuans); // Xóa tất cả các bình luận liên quan

                // Xóa bài đăng
                _context.BaiDangs.Remove(baiDang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaiDangExists(int id)
        {
            return _context.BaiDangs.Any(e => e.MaBaiDang == id);
        }
    }
}
