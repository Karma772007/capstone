using capstone.data;
using Microsoft.AspNetCore.Mvc;

namespace capstone.Controllers
{
    public class TechnicianController : Controller
    {

        private readonly Projectcontext _context;

        public TechnicianController(Projectcontext context)
        {
            _context = context;
        }


        public IActionResult MyAssignedMachinesTechnicianDashboard()
        {
            var x = _context.Machines.ToList();
            return View(x);
        }
        public IActionResult technichandashboard()
        {
            var x = _context.Machines.ToList();
            return View(x);
        }
        public IActionResult UpdateTasksTechnicianDashBoard()
        {
            var x = _context.Maintenancehistories.ToList();
            return View(x);
        }
        

    }
}
