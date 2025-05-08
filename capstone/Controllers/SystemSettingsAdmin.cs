using capstone.data;
using capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace capstone.Controllers
{
    public class SystemSettingsAdmin : Controller
    {
        private readonly Projectcontext _context;

        public SystemSettingsAdmin(Projectcontext context)
        {
            _context = context;
        }
        public IActionResult SystemSettings()
        {
            var x = _context.Users.ToList();
            return View(x);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var machine = await _context.Users.FindAsync(id);
            if (machine == null)
            {
                TempData["ErrorMessage"] = "not found.";
                return RedirectToAction(nameof(SystemSettings));
            }

            _context.Users.Remove(machine);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "deleted successfully.";
            return RedirectToAction(nameof(SystemSettings));
        }
        public IActionResult Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("SystemSettings");
        }


    }
}
