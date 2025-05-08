using capstone.data;
using capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace capstone.Controllers
{
    
        public class SparePartsAdminController : Controller
        {
            private readonly Projectcontext _context;

            public SparePartsAdminController(Projectcontext context)
            {
                _context = context;
            }
            public IActionResult SparePartsAdminDashboard()
            {
                var clean = _context.Sparepartinventories.ToList();
                return View(clean);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Delete(int id)
            {
                var machine = await _context.Sparepartinventories.FindAsync(id);
                if (machine == null)
                {
                    TempData["ErrorMessage"] = " not found.";
                    return RedirectToAction(nameof(SparePartsAdminDashboard));
                }

                _context.Sparepartinventories.Remove(machine);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "deleted successfully.";
                return RedirectToAction(nameof(SparePartsAdminDashboard));
            }

        public IActionResult Create(SparePartInventory spare)
        {
            _context.Sparepartinventories.Add(spare);
            _context.SaveChanges();
            return RedirectToAction("SparePartsAdminDashboard");
        }
    }
   }

