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
    public class BinhLuansController : Controller
    {
        private readonly ASPSTUDENTContext _context;

        public BinhLuansController(ASPSTUDENTContext context)
        {
            _context = context;
        }

        // GET: BinhLuans
        public async Task<IActionResult> Index(int? maBaiDang)
        {
            if (maBaiDang == null)
            {
                return NotFound();
            }

            // Lọc các bình luận có MaBaiDang giống với maBaiDang truyền vào
            var hoTroSinhVienHocTapContext = _context.BinhLuans
                .Include(b => b.BaiDang)
                .Include(b => b.NguoiDung)
                .Where(b => b.MaBaiDang == maBaiDang); // Thêm điều kiện lọc ở đây

            return View(await hoTroSinhVienHocTapContext.ToListAsync());
        }



        // GET: BinhLuans/Create
        public IActionResult Create(int? maBaiDang)
        {
            if (maBaiDang == null)
            {
                return NotFound();
            }

            var maNguoiDungString = HttpContext.Session.GetString("MaNguoiDung");
            if (maNguoiDungString == null || !int.TryParse(maNguoiDungString, out int maNguoiDung))
            {
                return RedirectToAction("Index", "DangNhaps");
            }

            var binhLuan = new BinhLuan
            {
                MaBaiDang = maBaiDang.Value,
                MaNguoiDung = maNguoiDung
            };
            // Truyền maBaiDang vào ViewBag
            ViewBag.MaBaiDang = maBaiDang.Value;

            return View(binhLuan);
        }


        // POST: BinhLuans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBinhLuan,MaBaiDang,MaNguoiDung,NoiDung,DuongDanAnh,NgayTao")] BinhLuan binhLuan)
        {
            _context.Add(binhLuan);
            await _context.SaveChangesAsync();
            // Chuyển hướng về trang danh sách bình luận với maBaiDang
            return RedirectToAction(nameof(Index), new { maBaiDang = binhLuan.MaBaiDang });
        }
       

        // GET: BinhLuans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var binhLuan = await _context.BinhLuans
                .Include(b => b.BaiDang)
                .Include(b => b.NguoiDung)
                .FirstOrDefaultAsync(m => m.MaBinhLuan == id);
            if (binhLuan == null)
            {
                return NotFound();
            }

            return View(binhLuan);
        }

        // POST: BinhLuans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var binhLuan = await _context.BinhLuans.FindAsync(id);
            if (binhLuan != null)
            {
                var maBaiDang = binhLuan.MaBaiDang; // Lưu giá trị maBaiDang để quay lại đúng danh sách
                _context.BinhLuans.Remove(binhLuan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { maBaiDang = maBaiDang }); // Redirect with maBaiDang
            }

            return NotFound();
        }


        private bool BinhLuanExists(int id)
        {
            return _context.BinhLuans.Any(e => e.MaBinhLuan == id);
        }
    }
}
