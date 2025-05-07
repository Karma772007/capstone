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

        public IActionResult MaintenanceAdminDshboard()
        {
            var clean = _context.Maintenancehistories.ToList();
            return View(clean);
        }

    }
}
