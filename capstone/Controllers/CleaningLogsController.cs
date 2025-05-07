using capstone.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace capstone.Controllers
{
    public class CleaningLogsController : Controller
    {
        private readonly Projectcontext _context;

        public CleaningLogsController(Projectcontext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var machine = await _context.Cleaninglogs.FindAsync(id);
            if (machine == null)
            {
                TempData["ErrorMessage"] = "Log not found.";
                return RedirectToAction(nameof(CleaningLogsAdminDashboard));
            }

            _context.Cleaninglogs.Remove(machine);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Log deleted successfully.";
            return RedirectToAction(nameof(CleaningLogsAdminDashboard));
        }

        public IActionResult CleaningLogsAdminDashboard()
        {
            var clean = _context.Cleaninglogs.ToList();
            return View(clean);
        }
    }
}
