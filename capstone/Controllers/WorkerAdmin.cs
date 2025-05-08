using capstone.data;
using Microsoft.AspNetCore.Mvc;

namespace capstone.Controllers
{
    public class WorkerAdmin : Controller
    {
        private readonly Projectcontext _context;

        public WorkerAdmin(Projectcontext context)
        {
            _context = context;
        }

        public async Task<IActionResult> WorkerAdminDashboard()
        {
            var machines = _context.Users.ToList();
            return View(machines);
        }


    }
}
