using Microsoft.AspNetCore.Mvc;

namespace capstone.Controllers
{
    public class MachineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
