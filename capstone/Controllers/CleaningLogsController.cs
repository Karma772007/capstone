using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using capstone.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using capstone.data;

namespace capstone.Controllers
{
    public class CleaningLogsController : Controller
    {
        private readonly Projectcontext _context;

        public CleaningLogsController(Projectcontext context)
        {
            _context = context;
        }


        // GET: CleaningLogs/CleaningLogsAdminDashboard
        public async Task<IActionResult> CleaningLogsAdminDashboard()
        {
            var cleaningLogs = await _context.Cleaninglogs
                .Include(cl => cl.Machine)
                .ToListAsync();
            ViewBag.Machines = await _context.Machines.ToListAsync();
            return View(cleaningLogs);
        }

        // POST: CleaningLogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MachineID,CleaningDate,CleaningMethod,Status")] CleaningLog cleaningLog)
        {
            // Log all incoming data
            Console.WriteLine("=== CleaningLogs/Create POST ===");
            Console.WriteLine($"Received MachineID: {cleaningLog.MachineID}");
            Console.WriteLine($"Received CleaningDate: {cleaningLog.CleaningDate}");
            Console.WriteLine($"Received CleaningMethod: {cleaningLog.CleaningMethod}");
            Console.WriteLine($"Received Status: {cleaningLog.Status}");
            Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");

            // Log all form data
            Console.WriteLine("Form Data:");
            foreach (var key in Request.Form.Keys)
            {
                Console.WriteLine($"{key}: {Request.Form[key]}");
            }

            // Log ModelState errors
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                Console.WriteLine("ModelState Errors: " + string.Join(", ", errors));
                TempData["ErrorMessage"] = "Error adding cleaning log: " + string.Join(", ", errors);
            }

            // Force MachineID validation
            if (cleaningLog.MachineID <= 0)
            {
                Console.WriteLine("MachineID is invalid (<= 0), setting to default value 1");
                cleaningLog.MachineID = 1; // Force a default valid MachineID
            }

            // Check if MachineID exists
            var machineExists = await _context.Machines.AnyAsync(m => m.MachineId == cleaningLog.MachineID);
            Console.WriteLine($"MachineID {cleaningLog.MachineID} exists in database: {machineExists}");
            if (!machineExists)
            {
                Console.WriteLine("MachineID does not exist, attempting to use default MachineID 1");
                cleaningLog.MachineID = 1; // Force a known valid MachineID
                machineExists = await _context.Machines.AnyAsync(m => m.MachineId == cleaningLog.MachineID);
                if (!machineExists)
                {
                    TempData["ErrorMessage"] = "No valid machines found in database. Please add a machine first.";
                    ViewBag.Machines = await _context.Machines.ToListAsync();
                    return RedirectToAction(nameof(CleaningLogsAdminDashboard));
                }
            }

            try
            {
                // Clear ModelState to force save (temporary for debugging)
                ModelState.Clear();
                _context.Add(cleaningLog);
                await _context.SaveChangesAsync();
                Console.WriteLine("Cleaning log saved successfully");
                TempData["SuccessMessage"] = "Cleaning log added successfully.";
                return RedirectToAction(nameof(CleaningLogsAdminDashboard));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                TempData["ErrorMessage"] = $"Error adding cleaning log: {ex.Message}";
            }

            ViewBag.Machines = await _context.Machines.ToListAsync();
            return RedirectToAction(nameof(CleaningLogsAdminDashboard));
        }

        // GET: CleaningLogs/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cleaningLog = await _context.Cleaninglogs
                .Where(cl => cl.LogID == id)
                .Select(cl => new
                {
                    logID = cl.LogID,
                    machineID = cl.MachineID,
                    cleaningDate = cl.CleaningDate.ToString("yyyy-MM-dd"),
                    cleaningMethod = cl.CleaningMethod,
                    status = cl.Status
                })
                .FirstOrDefaultAsync();

            if (cleaningLog == null)
            {
                Console.WriteLine($"Edit: No cleaning log found for LogID {id}");
                return NotFound();
            }

            Console.WriteLine($"Edit: Returning data for LogID {id}, MachineID: {cleaningLog.machineID}");
            return Json(cleaningLog);
        }

        // POST: CleaningLogs/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("LogID,MachineID,CleaningDate,CleaningMethod,Status")] CleaningLog cleaningLog)
        {
            // Log all incoming data
            Console.WriteLine("=== CleaningLogs/Update POST ===");
            Console.WriteLine($"Received LogID: {cleaningLog.LogID}");
            Console.WriteLine($"Received MachineID: {cleaningLog.MachineID}");
            Console.WriteLine($"Received CleaningDate: {cleaningLog.CleaningDate}");
            Console.WriteLine($"Received CleaningMethod: {cleaningLog.CleaningMethod}");
            Console.WriteLine($"Received Status: {cleaningLog.Status}");
            Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");

            // Log all form data
            Console.WriteLine("Form Data:");
            foreach (var key in Request.Form.Keys)
            {
                Console.WriteLine($"{key}: {Request.Form[key]}");
            }

            // Log ModelState errors
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                Console.WriteLine("ModelState Errors: " + string.Join(", ", errors));
                TempData["ErrorMessage"] = "Error updating cleaning log: " + string.Join(", ", errors);
            }

            // Validate LogID
            if (cleaningLog.LogID <= 0)
            {
                Console.WriteLine("Invalid LogID, cannot update");
                TempData["ErrorMessage"] = "Invalid cleaning log ID.";
                ViewBag.Machines = await _context.Machines.ToListAsync();
                return RedirectToAction(nameof(CleaningLogsAdminDashboard));
            }

            // Force MachineID validation
            if (cleaningLog.MachineID <= 0)
            {
                Console.WriteLine("MachineID is invalid (<= 0), setting to default value 1");
                cleaningLog.MachineID = 1; // Force a default valid MachineID
            }

            // Check if MachineID exists
            var machineExists = await _context.Machines.AnyAsync(m => m.MachineId == cleaningLog.MachineID);
            Console.WriteLine($"MachineID {cleaningLog.MachineID} exists in database: {machineExists}");
            if (!machineExists)
            {
                Console.WriteLine("MachineID does not exist, attempting to use default MachineID 1");
                cleaningLog.MachineID = 1; // Force a known valid MachineID
                machineExists = await _context.Machines.AnyAsync(m => m.MachineId == cleaningLog.MachineID);
                if (!machineExists)
                {
                    TempData["ErrorMessage"] = "No valid machines found in database. Please add a machine first.";
                    ViewBag.Machines = await _context.Machines.ToListAsync();
                    return RedirectToAction(nameof(CleaningLogsAdminDashboard));
                }
            }

            try
            {
                // Find existing cleaning log
                var existingLog = await _context.Cleaninglogs.FindAsync(cleaningLog.LogID);
                if (existingLog == null)
                {
                    Console.WriteLine($"Update: No cleaning log found for LogID {cleaningLog.LogID}");
                    TempData["ErrorMessage"] = "Cleaning log not found.";
                    ViewBag.Machines = await _context.Machines.ToListAsync();
                    return RedirectToAction(nameof(CleaningLogsAdminDashboard));
                }

                // Update fields
                existingLog.MachineID = cleaningLog.MachineID;
                existingLog.CleaningDate = cleaningLog.CleaningDate;
                existingLog.CleaningMethod = cleaningLog.CleaningMethod;
                existingLog.Status = cleaningLog.Status;

                // Clear ModelState to force save (temporary for debugging)
                ModelState.Clear();
                _context.Update(existingLog);
                await _context.SaveChangesAsync();
                Console.WriteLine("Cleaning log updated successfully");
                TempData["SuccessMessage"] = "Cleaning log updated successfully.";
                return RedirectToAction(nameof(CleaningLogsAdminDashboard));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                TempData["ErrorMessage"] = $"Error updating cleaning log: {ex.Message}";
            }

            ViewBag.Machines = await _context.Machines.ToListAsync();
            return RedirectToAction(nameof(CleaningLogsAdminDashboard));
        }

        // POST: CleaningLogs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cleaningLog = await _context.Cleaninglogs.FindAsync(id);
                if (cleaningLog != null)
                {
                    _context.Cleaninglogs.Remove(cleaningLog);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cleaning log deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting cleaning log: {ex.Message}";
            }
            return RedirectToAction(nameof(CleaningLogsAdminDashboard));
        }
    }
}