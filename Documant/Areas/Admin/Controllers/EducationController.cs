using Documant.Context;
using Documant.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Documant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController : Controller
    {
        private readonly PortfolioContext _context;

        public EducationController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult EducationList()
        {
            var values = _context.Educations.OrderBy(x => x.DisplayOrder).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateEducation()
        {
            return View("EducationForm", new Education());
        }

        [HttpPost]
        public IActionResult CreateEducation(Education model)
        {
            if (!ModelState.IsValid)
                return View("EducationForm", model);

            _context.Educations.Add(model);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim kaydı başarıyla eklendi.";
            return RedirectToAction("EducationList");
        }

        [HttpGet]
        public IActionResult UpdateEducation(int id)
        {
            var value = _context.Educations.Find(id);
            if (value == null) return NotFound();
            return View("EducationForm", value);
        }

        [HttpPost]
        public IActionResult UpdateEducation(Education model)
        {
            if (!ModelState.IsValid)
                return View("EducationForm", model);

            _context.Educations.Update(model);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim kaydı başarıyla güncellendi.";
            return RedirectToAction("EducationList");
        }

        [HttpPost]
        public IActionResult DeleteEducation(int id)
        {
            var value = _context.Educations.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Silinecek kayıt bulunamadı.";
                return RedirectToAction("EducationList");
            }

            _context.Educations.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim kaydı başarıyla silindi.";
            return RedirectToAction("EducationList");
        }
    }
}
