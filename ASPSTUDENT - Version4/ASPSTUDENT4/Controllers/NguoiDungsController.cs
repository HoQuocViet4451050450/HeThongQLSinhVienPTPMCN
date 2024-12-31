using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml; // Thư viện EPPlus
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

        // GET: NguoiDungs/Search
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            var nguoiDungs = from n in _context.NguoiDungs
                             select n;

            // Kiểm tra nếu có từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                nguoiDungs = nguoiDungs.Where(s => s.HoTen.Contains(searchString) ||
                                                   s.TenDangNhap.Contains(searchString) ||
                                                   s.LoaiNguoiDung.Contains(searchString));
                ViewData["SearchString"] = searchString; // Lưu từ khóa tìm kiếm để giữ trong ô tìm kiếm
            }

            return View(await nguoiDungs.ToListAsync());
        }

        // GET: NguoiDungs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var nguoiDung = await _context.NguoiDungs.FirstOrDefaultAsync(m => m.MaNguoiDung == id);
            if (nguoiDung == null) return NotFound();

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
                var existingUser = await _context.NguoiDungs
                    .FirstOrDefaultAsync(u => u.TenDangNhap == nguoiDung.TenDangNhap);

                if (existingUser != null)
                {
                    TempData["ErrorMessage"] = "Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.";
                    return View(nguoiDung);
                }

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
            if (id == null) return NotFound();

            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung == null) return NotFound();

            return View(nguoiDung);
        }

        // POST: NguoiDungs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNguoiDung,HoTen,TenDangNhap,MatKhau,LoaiNguoiDung,NgayTao")] NguoiDung nguoiDung)
        {
            if (id != nguoiDung.MaNguoiDung) return NotFound();

            try
            {
                var existingUser = await _context.NguoiDungs
                    .FirstOrDefaultAsync(u => u.TenDangNhap == nguoiDung.TenDangNhap && u.MaNguoiDung != nguoiDung.MaNguoiDung);

                if (existingUser != null)
                {
                    TempData["ErrorMessage"] = "Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.";
                    return View(nguoiDung);
                }

                _context.Update(nguoiDung);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Người dùng đã được cập nhật thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NguoiDungExists(nguoiDung.MaNguoiDung))
                    return NotFound();
                else
                    TempData["ErrorMessage"] = "Đã có lỗi khi cập nhật người dùng!";
            }

            return View(nguoiDung);
        }

        // GET: NguoiDungs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var nguoiDung = await _context.NguoiDungs
                .FirstOrDefaultAsync(m => m.MaNguoiDung == id);
            if (nguoiDung == null) return NotFound();

            return View(nguoiDung);
        }

        // POST: NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung != null) _context.NguoiDungs.Remove(nguoiDung);

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Người dùng đã được xóa thành công!";
            return RedirectToAction(nameof(Index));
        }

        private bool NguoiDungExists(int id)
        {
            return _context.NguoiDungs.Any(e => e.MaNguoiDung == id);
        }

        // Export Excel
        [HttpGet]
        public async Task<IActionResult> ExportData()
        {
            try
            {
                // Cấu hình License EPPlus
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Sử dụng phiên bản miễn phí

                var nguoiDungs = await _context.NguoiDungs.ToListAsync();

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Danh sách người dùng");

                    // Tiêu đề bảng
                    worksheet.Cells[1, 1].Value = "Mã người dùng";
                    worksheet.Cells[1, 2].Value = "Họ tên";
                    worksheet.Cells[1, 3].Value = "Tên đăng nhập";
                    worksheet.Cells[1, 4].Value = "Mật khẩu";
                    worksheet.Cells[1, 5].Value = "Loại người dùng";
                    worksheet.Cells[1, 6].Value = "Ngày tạo";

                    // Thêm dữ liệu vào Excel
                    for (int i = 0; i < nguoiDungs.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = nguoiDungs[i].MaNguoiDung;
                        worksheet.Cells[i + 2, 2].Value = nguoiDungs[i].HoTen;
                        worksheet.Cells[i + 2, 3].Value = nguoiDungs[i].TenDangNhap;
                        worksheet.Cells[i + 2, 4].Value = nguoiDungs[i].MatKhau;
                        worksheet.Cells[i + 2, 5].Value = nguoiDungs[i].LoaiNguoiDung;
                        worksheet.Cells[i + 2, 6].Value = nguoiDungs[i].NgayTao.HasValue
    ? nguoiDungs[i].NgayTao.Value.ToString("dd/MM/yyyy")
    : ""; // Nếu NgayTao là null, để trống ô trong Excel

                    }

                    // Lưu file Excel vào bộ nhớ
                    var fileBytes = package.GetAsByteArray();

                    // Trả về file Excel dưới dạng tệp tải xuống
                    return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "NguoiDungs.xlsx");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xuất dữ liệu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // Import Excel
        [HttpPost]
        public async Task<IActionResult> ImportData(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "File không hợp lệ!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var nguoiDung = new NguoiDung
                            {
                                HoTen = worksheet.Cells[row, 1].Text,
                                TenDangNhap = worksheet.Cells[row, 2].Text,
                                MatKhau = worksheet.Cells[row, 3].Text,
                                LoaiNguoiDung = worksheet.Cells[row, 4].Text,
                                NgayTao = DateTime.Now
                            };

                            _context.NguoiDungs.Add(nguoiDung);
                        }
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "Dữ liệu đã được nhập thành công!";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi nhập dữ liệu: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
