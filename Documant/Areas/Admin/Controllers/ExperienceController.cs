using Documant.Context;
using Documant.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Documant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperienceController : Controller
    {
        private readonly PortfolioContext _context;

        public ExperienceController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ExperienceList()
        {
            var values = _context.experiences.OrderByDescending(x => x.ExperienceId).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateExperience()
        {
            return View("ExperienceForm", new Experience());
        }

        [HttpPost]
        public IActionResult CreateExperience(Experience model)
        {
            if (!ModelState.IsValid)
                return View("ExperienceForm", model);

            _context.experiences.Add(model);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim başarıyla eklendi.";
            return RedirectToAction("ExperienceList");
        }

        [HttpGet]
        public IActionResult UpdateExperience(int id)
        {
            var value = _context.experiences.Find(id);
            if (value == null) return NotFound();
            return View("ExperienceForm", value);
        }

        [HttpPost]
        public IActionResult UpdateExperience(Experience model)
        {
            if (!ModelState.IsValid)
                return View("ExperienceForm", model);

            _context.experiences.Update(model);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim başarıyla güncellendi.";
            return RedirectToAction("ExperienceList");
        }

        [HttpPost]
        public IActionResult DeleteExperience(int id)
        {
            var value = _context.experiences.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Silinecek kayıt bulunamadı.";
                return RedirectToAction("ExperienceList");
            }

            _context.experiences.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim başarıyla silindi.";
            return RedirectToAction("ExperienceList");
        }
    }
}
