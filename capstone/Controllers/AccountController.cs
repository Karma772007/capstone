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
       


    }
}          