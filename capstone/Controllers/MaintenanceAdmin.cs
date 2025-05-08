using capstone.data;
using capstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Create(MaintenanceHistory maintenanceHistory)
        {
            _context.Maintenancehistories.Add(maintenanceHistory);
            _context.SaveChanges();
            return RedirectToAction("MaintenanceAdminDshboard");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var maintenance = await _context.Maintenancehistories.FindAsync(id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return Json(new
            {
                historyID = maintenance.HistoryID,
                machineID = maintenance.MachineID,
                repairDetails = maintenance.RepairDetails,
                completionDate = maintenance.CompletionDate
            });
        }
      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(MaintenanceHistory maintenance)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = string.Join("; ", errors);
                var machines = await _context.Maintenancehistories.ToListAsync();
                return View("MaintenanceAdminDshboard", machines);
            }

            try
            {
                var existingMachine = await _context.Maintenancehistories.FindAsync(maintenance.HistoryID);
                if (existingMachine == null)
                {
                    TempData["ErrorMessage"] = "History not found.";
                    return RedirectToAction(nameof(MaintenanceAdminDshboard));
                }

                existingMachine.MachineID = maintenance.MachineID;
                existingMachine.RepairDetails = maintenance.RepairDetails;
                existingMachine.CompletionDate = maintenance.CompletionDate;
              
                _context.Maintenancehistories.Update(existingMachine);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "History updated successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error updating History: {ex.Message}";
                var machines = await _context.Maintenancehistories.ToListAsync();
                return View("MaintenanceAdminDshboard", machines);
            }

            return RedirectToAction(nameof(MaintenanceAdminDshboard));
        }


    }
}
