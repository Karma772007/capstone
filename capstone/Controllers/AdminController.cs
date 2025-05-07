using capstone.data;
using capstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace capstone.Controllers
{
    public class AdminController : Controller
    {
        private readonly Projectcontext _context;

        public AdminController(Projectcontext context)
        {
            _context = context;
        }

        // عرض صفحة إدارة الماكينات
        public IActionResult MachineManagmentAdmin()
        {
            var machines = _context.Machines.ToList();
            return View(machines);
        }

        // إضافة ماكينة جديدة
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Machine machine)
        {
            if (!ModelState.IsValid)
            {
                // ترجع لنفس الصفحة مع عرض الماكينات لو في خطأ
                var machines = _context.Machines.ToList();
                return View("MachineManagmentAdmin", machines);
            }

            _context.Machines.Add(machine);
            _context.SaveChanges();

            return RedirectToAction("MachineManagmentAdmin");
        }

        // حذف ماكينة
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
            {
                TempData["ErrorMessage"] = "Machine not found.";
                return RedirectToAction(nameof(MachineManagmentAdmin));
            }

            _context.Machines.Remove(machine);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Machine deleted successfully.";
            return RedirectToAction(nameof(MachineManagmentAdmin));
        }

        public IActionResult CleaningLogsAdminDashboard()
        {
            var clean = _context.Cleaninglogs.ToList();
            return View(clean);
        }

    }
}
