using capstone.data;
using capstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace capstone.Controllers
{
    public class AccountController : Controller
    {
        private readonly Projectcontext _context;
        public AccountController(Projectcontext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user, string ConfirmPassword)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return View(user);
            }

            if (user.Password != ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return View(user);
            }

            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(user);
            }

            if (await _context.Users.AnyAsync(u => u.UserName == user.UserName))
            {
                ModelState.AddModelError("UserName", "Username is already taken.");
                return View(user);
            }

            try
            {
                user.Password = HashPassword(user.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                if (user.Role == "worker")
                {
                    return RedirectToAction("WorkerDashboard", "Worker");
                }
                else if (user.Role == "technician")
                {
                    return RedirectToAction("TechnicianDashboard", "Technician");
                }
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while registering. Please try again.");
                return View(user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "Please fill in all fields.";
                return View("Login");
            }

            string hashedPassword = HashPassword(password);

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == hashedPassword);

            if (user == null)
            {
                TempData["Error"] = "Invalid email or password.";
                return View("Login");
            }

         

            if (user.Role == "worker")
            {
                return RedirectToAction("WorkerDashboard", "Worker");
            }
            else if (user.Role == "technician")
            {
                return RedirectToAction("TechnicianDashboard", "Technician");
            }

            return RedirectToAction("Index", "Home");
        }




        private string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty.");
            }
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}          