using capstone.data;
using capstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace capstone.Controllers
{
    public class AdminController : Controller
    {
        private readonly Projectcontext _context;

        public AdminController(Projectcontext context)
        {
            _context = context;
        }

        public async Task<IActionResult> MachineManagmentAdmin()
        {
            var machines = await _context.Machines.ToListAsync();
            return View(machines);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Machine machine)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = string.Join("; ", errors);
                var machines = await _context.Machines.ToListAsync();
                return View("MachineManagmentAdmin", machines);
            }

            try
            {
                _context.Machines.Add(machine);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Machine added successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error saving machine: {ex.Message}";
                var machines = await _context.Machines.ToListAsync();
                return View("MachineManagmentAdmin", machines);
            }

            return RedirectToAction(nameof(MachineManagmentAdmin));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
            {
                TempData["ErrorMessage"] = "Machine not found.";
                return RedirectToAction(nameof(MachineManagmentAdmin));
            }

            return Json(machine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Machine machine)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = string.Join("; ", errors);
                var machines = await _context.Machines.ToListAsync();
                return View("MachineManagmentAdmin", machines);
            }

            try
            {
                var existingMachine = await _context.Machines.FindAsync(machine.MachineId);
                if (existingMachine == null)
                {
                    TempData["ErrorMessage"] = "Machine not found.";
                    return RedirectToAction(nameof(MachineManagmentAdmin));
                }

                existingMachine.Name = machine.Name;
                existingMachine.SerialNumber = machine.SerialNumber;
                existingMachine.Location = machine.Location;
                existingMachine.Status = machine.Status;
                existingMachine.LastMaintenanceDate = machine.LastMaintenanceDate;

                _context.Machines.Update(existingMachine);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Machine updated successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error updating machine: {ex.Message}";
                var machines = await _context.Machines.ToListAsync();
                return View("MachineManagmentAdmin", machines);
            }

            return RedirectToAction(nameof(MachineManagmentAdmin));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
            {
                TempData["ErrorMessage"] = "Machine not found.";
                return RedirectToAction(nameof(MachineManagmentAdmin));
            }

            try
            {
                _context.Machines.Remove(machine);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Machine deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting machine: {ex.Message}";
            }

            return RedirectToAction(nameof(MachineManagmentAdmin));
        }

        public IActionResult admindashboard()
        {
            ViewBag.Maintenancehistories = _context.Maintenancehistories.ToList();
            ViewBag.Machines = _context.Machines.ToList();
            return View();

        }
    }


}