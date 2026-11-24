using Documant.Context;
using Documant.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Documant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult SkillList()
        {
            var values = _context.skills.OrderByDescending(x => x.Level).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSkill()
        {
            return View("SkillForm", new Skill());
        }

        [HttpPost]
        public IActionResult CreateSkill(Skill model)
        {
            if (!ModelState.IsValid)
                return View("SkillForm", model);

            _context.skills.Add(model);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek başarıyla eklendi.";
            return RedirectToAction("SkillList");
        }

        [HttpGet]
        public IActionResult UpdateSkill(int id)
        {
            var value = _context.skills.Find(id);
            if (value == null) return NotFound();
            return View("SkillForm", value);
        }

        [HttpPost]
        public IActionResult UpdateSkill(Skill model)
        {
            if (!ModelState.IsValid)
                return View("SkillForm", model);

            _context.skills.Update(model);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek başarıyla güncellendi.";
            return RedirectToAction("SkillList");
        }

        [HttpPost]
        public IActionResult DeleteSkill(int id)
        {
            var value = _context.skills.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Silinecek kayıt bulunamadı.";
                return RedirectToAction("SkillList");
            }

            _context.skills.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek başarıyla silindi.";
            return RedirectToAction("SkillList");
        }
    }
}
