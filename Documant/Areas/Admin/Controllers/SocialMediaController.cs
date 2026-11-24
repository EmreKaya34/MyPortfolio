using Documant.Context;
using Documant.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Documant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialMediaController : Controller
    {
        private readonly PortfolioContext _context;

        public SocialMediaController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult SocialMediaList()
        {
            var values = _context.SocialMedias.OrderBy(x => x.SocialMediaId).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSocialMedia()
        {
            return View("SocialMediaForm", new SocialMedia());
        }

        [HttpPost]
        public IActionResult CreateSocialMedia(SocialMedia model)
        {
            if (!ModelState.IsValid)
                return View("SocialMediaForm", model);

            _context.SocialMedias.Add(model);
            _context.SaveChanges();
            TempData["Success"] = "Sosyal medya bağlantısı başarıyla eklendi.";
            return RedirectToAction("SocialMediaList");
        }

        [HttpGet]
        public IActionResult UpdateSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value == null) return NotFound();
            return View("SocialMediaForm", value);
        }

        [HttpPost]
        public IActionResult UpdateSocialMedia(SocialMedia model)
        {
            if (!ModelState.IsValid)
                return View("SocialMediaForm", model);

            _context.SocialMedias.Update(model);
            _context.SaveChanges();
            TempData["Success"] = "Sosyal medya bağlantısı başarıyla güncellendi.";
            return RedirectToAction("SocialMediaList");
        }

        [HttpPost]
        public IActionResult DeleteSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Silinecek kayıt bulunamadı.";
                return RedirectToAction("SocialMediaList");
            }

            _context.SocialMedias.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Sosyal medya bağlantısı başarıyla silindi.";
            return RedirectToAction("SocialMediaList");
        }
    }
}
