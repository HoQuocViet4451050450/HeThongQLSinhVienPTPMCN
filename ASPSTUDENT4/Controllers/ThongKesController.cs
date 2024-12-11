using ASPSTUDENT4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPSTUDENT4.Controllers
{
    public class ThongKesController : Controller
    {
        private readonly ASPSTUDENTContext _context;

        public ThongKesController(ASPSTUDENTContext context)
        {
            _context = context;
        }

        // GET: ThongKes/GpaByClass
        public async Task<IActionResult> GpaByClass()
        {
            // Get the GPA grouped by class
            var gpaStats = await _context.ChiTietSinhViens
                .GroupBy(sv => sv.LopHoc.TenLop)
                .Select(group => new
                {
                    ClassName = group.Key,
                    AverageGPA = group.Average(sv => sv.DiemGPA)
                })
                .ToListAsync();

            // You can modify this query to fetch more detailed stats like GPA distribution by class
            // Return the statistics to a view
            return View(gpaStats);
        }
    }
}
