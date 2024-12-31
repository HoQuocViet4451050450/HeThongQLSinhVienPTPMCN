using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPSTUDENT4.Data;
using ASPSTUDENT4.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;



namespace ASPSTUDENT4.Controllers
{
    public class ChiTietSinhViensController : Controller
    {
        private readonly ASPSTUDENTContext _context;

        public ChiTietSinhViensController(ASPSTUDENTContext context)
        {
            _context = context;
        }

        // GET: ChiTietSinhViens
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            // Xác định trạng thái sắp xếp
            ViewData["GpaSortParam"] = sortOrder == "GPA_Asc" ? "GPA_Desc" : "GPA_Asc";
            ViewData["CurrentFilter"] = searchString;

            // Lấy danh sách sinh viên bao gồm thông tin lớp học và người dùng
            var chiTietSinhViensQuery = _context.ChiTietSinhViens
                                                .Include(c => c.LopHoc)
                                                .Include(c => c.NguoiDung)
                                                .AsQueryable();

            // Lọc theo mã sinh viên nếu có tham số tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                chiTietSinhViensQuery = chiTietSinhViensQuery.Where(s => s.MaSinhVien.ToString().Contains(searchString));
            }

            // Áp dụng sắp xếp dựa trên tham số sortOrder
            chiTietSinhViensQuery = sortOrder switch
            {
                "GPA_Asc" => chiTietSinhViensQuery.OrderBy(s => s.DiemGPA),
                "GPA_Desc" => chiTietSinhViensQuery.OrderByDescending(s => s.DiemGPA),
                _ => chiTietSinhViensQuery.OrderBy(s => s.MaSinhVien)
            };

            // Lấy danh sách sinh viên mà không phân trang
            var chiTietSinhViens = await chiTietSinhViensQuery.ToListAsync();

            return View(chiTietSinhViens);
        }

        // GET: ChiTietSinhViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietSinhVien = await _context.ChiTietSinhViens
                .Include(c => c.LopHoc)
                .Include(c => c.NguoiDung)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (chiTietSinhVien == null)
            {
                return NotFound();
            }

            return View(chiTietSinhVien);
        }

        // GET: ChiTietSinhViens/Create
        public IActionResult Create()
        {
            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "TenLop"); // Hiển thị tên lớp thay vì mã lớp
            ViewData["MaSinhVien"] = new SelectList(_context.NguoiDungs, "MaNguoiDung", "HoTen"); // Hiển thị tên sinh viên
            return View();
        }

        // POST: ChiTietSinhViens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSinhVien,MaLop,QueQuan,SoDienThoai,TongTinChi,DiemGPA")] ChiTietSinhVien chiTietSinhVien)
        {
            try
            {
                _context.Add(chiTietSinhVien);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Sinh viên đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Sinh viên đã được tạo không thành công! Lỗi: " + ex.Message;
            }

            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "TenLop", chiTietSinhVien.MaLop);
            ViewData["MaSinhVien"] = new SelectList(_context.NguoiDungs, "MaNguoiDung", "HoTen", chiTietSinhVien.MaSinhVien);
            return View(chiTietSinhVien);
        }

        // GET: ChiTietSinhViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietSinhVien = await _context.ChiTietSinhViens.FindAsync(id);
            if (chiTietSinhVien == null)
            {
                return NotFound();
            }
            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "TenLop", chiTietSinhVien.MaLop);
            ViewData["MaSinhVien"] = new SelectList(_context.NguoiDungs, "MaNguoiDung", "HoTen", chiTietSinhVien.MaSinhVien);
            return View(chiTietSinhVien);
        }

        // POST: ChiTietSinhViens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSinhVien,MaLop,QueQuan,SoDienThoai,TongTinChi,DiemGPA")] ChiTietSinhVien chiTietSinhVien)
        {
            if (id != chiTietSinhVien.MaSinhVien)
            {
                return NotFound();
            }

            try
            {
                _context.Update(chiTietSinhVien);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietSinhVienExists(chiTietSinhVien.MaSinhVien))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ChiTietSinhViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietSinhVien = await _context.ChiTietSinhViens
                .Include(c => c.LopHoc)
                .Include(c => c.NguoiDung)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (chiTietSinhVien == null)
            {
                return NotFound();
            }

            return View(chiTietSinhVien);
        }

        // POST: ChiTietSinhViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietSinhVien = await _context.ChiTietSinhViens.FindAsync(id);
            if (chiTietSinhVien != null)
            {
                _context.ChiTietSinhViens.Remove(chiTietSinhVien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietSinhVienExists(int id)
        {
            return _context.ChiTietSinhViens.Any(e => e.MaSinhVien == id);
        }

        // Export Excel
        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            try
            {
                // Cấu hình License EPPlus
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var sinhViens = await _context.ChiTietSinhViens.ToListAsync();

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Danh sách sinh viên");

                    // Tiêu đề bảng
                    worksheet.Cells[1, 1].Value = "Mã sinh viên";
                    worksheet.Cells[1, 2].Value = "Quê quán"; // Chuyển cột Quê quán sang cột 2
                    worksheet.Cells[1, 3].Value = "Số điện thoại"; // Chuyển Số điện thoại sang cột 3
                    worksheet.Cells[1, 4].Value = "Tổng tín chỉ"; // Chuyển Tổng tín chỉ sang cột 4
                    worksheet.Cells[1, 5].Value = "Điểm GPA"; // Chuyển Điểm GPA sang cột 5

                    // Thêm dữ liệu vào Excel
                    for (int i = 0; i < sinhViens.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = sinhViens[i].MaSinhVien;
                        worksheet.Cells[i + 2, 2].Value = sinhViens[i].QueQuan;
                        worksheet.Cells[i + 2, 3].Value = sinhViens[i].SoDienThoai;
                        worksheet.Cells[i + 2, 4].Value = sinhViens[i].TongTinChi;
                        worksheet.Cells[i + 2, 5].Value = sinhViens[i].DiemGPA;
                    }

                    // Lưu file Excel vào bộ nhớ
                    var fileBytes = package.GetAsByteArray();

                    // Trả về file Excel dưới dạng tệp tải xuống
                    return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachSinhVien.xlsx");
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
        public async Task<IActionResult> ImportSinhVien(IFormFile file)
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
                            var sinhVien = new ChiTietSinhVien
                            {
                                MaSinhVien = int.TryParse(worksheet.Cells[row, 1].Text, out var maSinhVien) ? maSinhVien : 0,
                                QueQuan = worksheet.Cells[row, 2].Text, // Lấy dữ liệu từ cột 2 (Quê quán)
                                SoDienThoai = worksheet.Cells[row, 3].Text, // Lấy dữ liệu từ cột 3 (Số điện thoại)
                                TongTinChi = int.TryParse(worksheet.Cells[row, 4].Text, out var tinChi) ? tinChi : 0,
                                DiemGPA = decimal.TryParse(worksheet.Cells[row, 5].Text, out var gpa) ? gpa : 0m
                            };

                            _context.ChiTietSinhViens.Add(sinhVien);
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
