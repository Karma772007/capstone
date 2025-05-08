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

        public async Task<IActionResult> Delete(int id)
        {
            var machine = await _context.Maintenancehistories.FindAsync(id);
            if (machine == null)
            {
                TempData["ErrorMessage"] = "History not found.";
                return RedirectToAction(nameof(UpdateTasksTechnicianDashBoard));
            }

            try
            {
                _context.Maintenancehistories.Remove(machine);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "History deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting History: {ex.Message}";
            }

            return RedirectToAction(nameof(UpdateTasksTechnicianDashBoard));
        }


    }
}
