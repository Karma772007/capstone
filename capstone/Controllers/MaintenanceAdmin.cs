using capstone.data;
using capstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace capstone.Controllers
{
    public class MaintenanceAdminController : Controller
    {
        private readonly Projectcontext _context;

        public MaintenanceAdminController(Projectcontext context)
        {
            _context = context;
        }

        public IActionResult MaintenanceAdminDshboard()
        {
            var maintenanceHistories = _context.Maintenancehistories.ToList();
            return View(maintenanceHistories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MaintenanceHistory maintenanceHistory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Maintenancehistories.Add(maintenanceHistory);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Maintenance request created successfully.";
                    return RedirectToAction(nameof(MaintenanceAdminDshboard));
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["ErrorMessage"] = string.Join("; ", errors);
                return RedirectToAction(nameof(MaintenanceAdminDshboard));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error creating maintenance request: {ex.Message}";
                return RedirectToAction(nameof(MaintenanceAdminDshboard));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var maintenance = await _context.Maintenancehistories.FindAsync(id);
                if (maintenance == null)
                {
                    return Json(new { success = false, message = "Maintenance history not found." });
                }

                return Json(new
                {
                    success = true,
                    historyID = maintenance.HistoryID,
                    machineID = maintenance.MachineID,
                    repairDetails = maintenance.RepairDetails,
                    completionDate = maintenance.CompletionDate?.ToString("yyyy-MM-ddTHH:mm")
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error fetching maintenance history: {ex.Message}" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] MaintenanceHistory maintenance)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { success = false, message = "Invalid data: " + string.Join("; ", errors) });
                }

                var existingMaintenance = await _context.Maintenancehistories.FindAsync(maintenance.HistoryID);
                if (existingMaintenance == null)
                {
                    return Json(new { success = false, message = "Maintenance history not found." });
                }

                existingMaintenance.MachineID = maintenance.MachineID;
                existingMaintenance.RepairDetails = maintenance.RepairDetails;
                existingMaintenance.CompletionDate = maintenance.CompletionDate;

                _context.Maintenancehistories.Update(existingMaintenance);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Maintenance history updated successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error updating maintenance history: {ex.Message}" });
            }
        }
    }
}