using capstone.data;
using Microsoft.AspNetCore.Mvc;

namespace capstone.Controllers
{
    public class MaintenanceAdmin : Controller
    {
        private readonly Projectcontext _context;

        public MaintenanceAdmin(Projectcontext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var machine = await _context.Maintenancehistories.FindAsync(id);
            if (machine == null)
            {
                TempData["ErrorMessage"] = "History not found.";
                return RedirectToAction(nameof(MaintenanceAdminDshboard));
            }

            _context.Maintenancehistories.Remove(machine);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "History deleted successfully.";
            return RedirectToAction(nameof(MaintenanceAdminDshboard));
        }

        public IActionResult MaintenanceAdminDshboard()
        {
            var clean = _context.Maintenancehistories.ToList();
            return View(clean);
        }
    }
}
