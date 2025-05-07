using capstone.data;
using Microsoft.AspNetCore.Mvc;

namespace capstone.Controllers
{
    public class SparePartsAdmin : Controller
    {
        public class SparePartsAdminController : Controller
        {
            private readonly Projectcontext _context;

            public SparePartsAdminController(Projectcontext context)
            {
                _context = context;
            }
            public ActionResult SparePartsAdminDashboard()
            {
                var clean = _context.Sparepartinventories.ToList();
                return View("SparePartsAdminDashboard");
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

           
        }
    }
}
