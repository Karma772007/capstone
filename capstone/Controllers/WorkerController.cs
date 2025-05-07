using Microsoft.AspNetCore.Mvc;

namespace capstone.Controllers
{
    public class WorkerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
