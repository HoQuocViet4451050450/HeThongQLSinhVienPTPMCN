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
    public class NguoiDungsController : Controller
    {
        private readonly ASPSTUDENTContext _context;

        public NguoiDungsController(ASPSTUDENTContext context)
        {
            _context = context;
        }

        // GET: NguoiDungs
        public async Task<IActionResult> Index()
        {
            return View(await _context.NguoiDungs.ToListAsync());
        }

        // GET: NguoiDungs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs
                .FirstOrDefaultAsync(m => m.MaNguoiDung == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // GET: NguoiDungs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NguoiDungs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNguoiDung,HoTen,TenDangNhap,MatKhau,LoaiNguoiDung,NgayTao")] NguoiDung nguoiDung)
        {
            try
            {
                // Kiểm tra xem tên đăng nhập đã tồn tại chưa
                var existingUser = await _context.NguoiDungs
                    .FirstOrDefaultAsync(u => u.TenDangNhap == nguoiDung.TenDangNhap);

                if (existingUser != null)
                {
                    // Nếu tên đăng nhập đã tồn tại, trả về thông báo lỗi
                    TempData["ErrorMessage"] = "Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.";
                    return View(nguoiDung);
                }

                // Nếu không có lỗi, thêm người dùng mới vào cơ sở dữ liệu
                _context.Add(nguoiDung);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Người dùng đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã có lỗi xảy ra khi tạo người dùng: " + ex.Message;
            }

            return View(nguoiDung);
        }

        // GET: NguoiDungs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return View(nguoiDung);
        }

        // POST: NguoiDungs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNguoiDung,HoTen,TenDangNhap,MatKhau,LoaiNguoiDung,NgayTao")] NguoiDung nguoiDung)
        {
            if (id != nguoiDung.MaNguoiDung)
            {
                return NotFound();
            }

            try
            {
                // Kiểm tra xem tên đăng nhập mới có trùng với tên đăng nhập của người dùng khác không
                var existingUser = await _context.NguoiDungs
                    .FirstOrDefaultAsync(u => u.TenDangNhap == nguoiDung.TenDangNhap && u.MaNguoiDung != nguoiDung.MaNguoiDung);

                if (existingUser != null)
                {
                    // Nếu tên đăng nhập đã tồn tại, trả về thông báo lỗi
                    TempData["ErrorMessage"] = "Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.";
                    return View(nguoiDung);
                }

                // Cập nhật thông tin người dùng
                _context.Update(nguoiDung);
                await _context.SaveChangesAsync();

                // Hiển thị thông báo thành công
                TempData["SuccessMessage"] = "Người dùng đã được cập nhật thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NguoiDungExists(nguoiDung.MaNguoiDung))
                {
                    return NotFound();
                }
                else
                {
                    TempData["ErrorMessage"] = "Đã có lỗi khi cập nhật người dùng!";
                    throw;
                }
            }
        }

        // GET: NguoiDungs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs
                .FirstOrDefaultAsync(m => m.MaNguoiDung == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // POST: NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung != null)
            {
                _context.NguoiDungs.Remove(nguoiDung);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Người dùng đã được xóa thành công!";
            return RedirectToAction(nameof(Index));
        }

        private bool NguoiDungExists(int id)
        {
            return _context.NguoiDungs.Any(e => e.MaNguoiDung == id);
        }
    }
}
