﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPSTUDENT.Data;

namespace ASPSTUDENT.Controllers
{
    public class SinhViensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SinhViensController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SinhViens.Include(s => s.LopHoc);
            return View(await applicationDbContext.ToListAsync());
        }

        //Detail
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens
                .Include(s => s.LopHoc)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        //Create
        public IActionResult Create()
        {
            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "MaLop");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSinhVien,HoTen,NgaySinh,GioiTinh,SoDienThoai,MaLop,TongTinChi,GPA,TrangThai,QueQuan")] SinhVien sinhVien)
        {
            try
            {
                _context.Add(sinhVien);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Sinh viên đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("PK_SinhViens") == true)
                {
                    ModelState.AddModelError("MaSinhVien", "Mã sinh viên đã tồn tại.");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi xảy ra khi lưu thông tin.");
                }

                ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "MaLop", sinhVien.MaLop);
                return View(sinhVien);
            }
        }


        //Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "MaLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSinhVien,HoTen,NgaySinh,GioiTinh,SoDienThoai,MaLop,TongTinChi,GPA,TrangThai,QueQuan")] SinhVien sinhVien)
        {
            if (id != sinhVien.MaSinhVien)
            {
                return NotFound();
            }

            try
            {
                _context.Update(sinhVien);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thông tin sinh viên đã được cập nhật!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SinhVienExists(sinhVien.MaSinhVien))
                {
                    return NotFound();
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật thông tin sinh viên.";
                    throw;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi: " + ex.Message;
            }

            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "MaLop", sinhVien.MaLop);
            return View(sinhVien);
        }


           
        //Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens
                .Include(s => s.LopHoc)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien != null)
            {
                try
                {
                    _context.SinhViens.Remove(sinhVien);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Sinh viên đã được xóa thành công!";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Đã có lỗi xảy ra khi xóa sinh viên: " + ex.Message;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Index(string searchQuery)
        {
            // Lấy danh sách sinh viên cùng lớp học
            var sinhViens = _context.SinhViens.Include(s => s.LopHoc).AsQueryable();

            // Nếu có từ khóa tìm kiếm, lọc dữ liệu
            if (!string.IsNullOrEmpty(searchQuery))
            {
                sinhViens = sinhViens.Where(s =>
                    s.MaSinhVien.Contains(searchQuery) ||
                    s.HoTen.Contains(searchQuery) ||
                    s.QueQuan.Contains(searchQuery));
            } 

            // Truyền từ khóa tìm kiếm để hiển thị lại trong giao diện
            ViewBag.SearchQuery = searchQuery;



            return View(await sinhViens.ToListAsync());
        }


        private bool SinhVienExists(string id)
        {
            return _context.SinhViens.Any(e => e.MaSinhVien == id);
        }
    }
}
