using Documant.Areas.Admin.Models;
using Documant.Context;
using Documant.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Documant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeroTitleController : Controller
    {
        private readonly PortfolioContext _context;

        public HeroTitleController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult HeroTitleList()
        {
            var values = _context.heroTitles.OrderBy(x => x.Order).ThenBy(x => x.HeroTitleId).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateHeroTitle()
        {
            return View("HeroTitleForm", new HeroTitleFormViewModel());
        }

        [HttpPost]
        public IActionResult CreateHeroTitle(HeroTitleFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("HeroTitleForm", model);

            var entity = new HeroTitle();
            MapToEntity(model, entity);
            _context.heroTitles.Add(entity);
            _context.SaveChanges();
            TempData["Success"] = "Unvan başarıyla eklendi.";
            return RedirectToAction("HeroTitleList");
        }

        [HttpGet]
        public IActionResult UpdateHeroTitle(int id)
        {
            var value = _context.heroTitles.Find(id);
            if (value == null) return NotFound();

            return View("HeroTitleForm", new HeroTitleFormViewModel
            {
                HeroTitleId = value.HeroTitleId,
                Title = value.Title,
                Order = value.Order,
                IsActive = value.IsActive
            });
        }

        [HttpPost]
        public IActionResult UpdateHeroTitle(HeroTitleFormViewModel model)
        {
            var entity = _context.heroTitles.Find(model.HeroTitleId);
            if (entity == null) return NotFound();

            if (!ModelState.IsValid)
                return View("HeroTitleForm", model);

            MapToEntity(model, entity);
            _context.SaveChanges();
            TempData["Success"] = "Unvan başarıyla güncellendi.";
            return RedirectToAction("HeroTitleList");
        }

        [HttpPost]
        public IActionResult DeleteHeroTitle(int id)
        {
            var value = _context.heroTitles.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Silinecek kayıt bulunamadı.";
                return RedirectToAction("HeroTitleList");
            }

            _context.heroTitles.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Unvan başarıyla silindi.";
            return RedirectToAction("HeroTitleList");
        }

        private static void MapToEntity(HeroTitleFormViewModel model, HeroTitle entity)
        {
            entity.Title = model.Title.Trim();
            entity.Order = model.Order;
            entity.IsActive = model.IsActive;
        }
    }
}
