using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace capstone.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
