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
    public class LopHocsController : Controller
    {
        private readonly ASPSTUDENTContext _context;

        public LopHocsController(ASPSTUDENTContext context)
        {
            _context = context;
        }

        // GET: LopHocs
        public async Task<IActionResult> Index()
        {
            return View(await _context.LopHocs.ToListAsync());
        }

        // GET: LopHocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lopHoc = await _context.LopHocs
                .FirstOrDefaultAsync(m => m.MaLop == id);
            if (lopHoc == null)
            {
                return NotFound();
            }

            return View(lopHoc);
        }

        // GET: LopHocs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LopHocs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLop,TenLop")] LopHoc lopHoc)
        {
            try
            {
                _context.Add(lopHoc);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Lớp học đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lớp học đã được tạo không thành công!";
            }

            return View(lopHoc);
        }

        // GET: LopHocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lopHoc = await _context.LopHocs.FindAsync(id);
            if (lopHoc == null)
            {
                return NotFound();
            }
            return View(lopHoc);
        }

        // POST: LopHocs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLop,TenLop")] LopHoc lopHoc)
        {
            if (id != lopHoc.MaLop)
            {
                return NotFound();
            }

            try
            {
                _context.Update(lopHoc);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Lớp học đã được cập nhật thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LopHocExists(lopHoc.MaLop))
                {
                    return NotFound();
                }
                else
                {
                    TempData["ErrorMessage"] = "Đã có lỗi khi cập nhật lớp học!";
                    throw;
                }
            }
        }

        // GET: LopHocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lopHoc = await _context.LopHocs
                .FirstOrDefaultAsync(m => m.MaLop == id);
            if (lopHoc == null)
            {
                return NotFound();
            }

            return View(lopHoc);
        }

        // POST: LopHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lopHoc = await _context.LopHocs.FindAsync(id);
            if (lopHoc != null)
            {
                _context.LopHocs.Remove(lopHoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Phương thức lọc sinh viên theo lớp
        public IActionResult LocSinhVienTheoLop(int maLop)
        {
            var sinhVien = _context.ChiTietSinhViens
                .Where(sv => sv.MaLop == maLop)   // Lọc sinh viên theo mã lớp
                .Include(sv => sv.NguoiDung)      // Bao gồm thông tin người dùng (nếu có)
                .ToList();

            if (sinhVien == null || !sinhVien.Any())
            {
                TempData["ErrorMessage"] = "Không tìm thấy sinh viên trong lớp này!";
                return RedirectToAction(nameof(Index));
            }

            return View(sinhVien);  // Trả về danh sách sinh viên cho view
        }

        // Kiểm tra lớp học tồn tại
        private bool LopHocExists(int id)
        {
            return _context.LopHocs.Any(e => e.MaLop == id);
        }
    }
}
