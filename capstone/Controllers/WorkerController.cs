using capstone.data;
using capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace capstone.Controllers
{
    public class WorkerController : Controller
    {
        private readonly Projectcontext _context;

        public WorkerController(Projectcontext context)
        {
            _context = context;
        }
        public IActionResult workerdashboard()
        {
            ViewBag.Maintenancehistories = _context.Maintenancehistories.ToList();
            ViewBag.Machines = _context.Machines.ToList();
            return View();

        }

        public IActionResult MyTasksWorkerDshboard()
        {
            var x =_context.Workorders.ToList();
            return View(x);
        }

        public IActionResult MaintenanceRequestsWorkerDashBoard()
        {
            var x = _context.Maintenancehistories.ToList();
            return View(x);
        }
        public IActionResult Create(MaintenanceHistory maintenanceHistory)
        {
            _context.Maintenancehistories.Add(maintenanceHistory);
            _context.SaveChanges();
            return RedirectToAction("MaintenanceRequestsWorkerDashBoard");
        }
    }
}
