using Microsoft.AspNetCore.Mvc;

namespace capstone.Controllers
{
    public class TechnicianController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
