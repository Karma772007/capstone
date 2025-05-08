using capstone.data;
using capstone.Models;
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

        public IActionResult Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("WorkerAdminDashboard");
        }



    }
}
