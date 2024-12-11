using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> Index()
        {
            var hoTroSinhVienHocTapContext = _context.BaiDangs.Include(b => b.NguoiDung);
            return View(await hoTroSinhVienHocTapContext.ToListAsync());
        }

        // GET: BaiDangs/Create
        public IActionResult Create()
        {
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDungs, "MaNguoiDung", "HoTen");
            return View();
        }

        // POST: BaiDangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBaiDang, NoiDung, DuongDanAnh, NgayTao")] BaiDang baiDang)
        {
            if (baiDang.DuongDanAnh == null)
            {
                // Nếu không có ảnh, có thể gán giá trị mặc định
                baiDang.DuongDanAnh = ""; // Hoặc gán giá trị khác như URL mặc định
            }
            // Lấy MaNguoiDung từ session (dưới dạng string)
            var userIdString = HttpContext.Session.GetString("MaNguoiDung");

            if (userIdString == null)
            {
                // Nếu không có MaNguoiDung trong session, chuyển hướng về trang đăng nhập hoặc thông báo lỗi
                return RedirectToAction("Index", "DangNhaps");
            }

            // Chuyển đổi userIdString sang int để gán cho MaNguoiDung
            if (int.TryParse(userIdString, out int userId))
            {
                baiDang.MaNguoiDung = userId;  // Gán MaNguoiDung cho bài đăng
            }
            else
            {
                // Nếu không chuyển đổi được, có thể trả về thông báo lỗi hoặc chuyển hướng
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
            var baiDang = await _context.BaiDangs.FindAsync(id);
            if (baiDang != null)
            {
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
