using Microsoft.AspNetCore.Mvc;

namespace Documant.Controllers
{
    public class ExperienceController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Default", null, "experience");
        }
    }
}
