using Documant.Context;
using Documant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Documant.ViewCompanents.DefaultViewCompanents
{
    public class _DefaultAboutComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultAboutComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var about = _context.Abouts.FirstOrDefault();
            // Hakkımda fotoğrafı yoksa ana sayfa fotoğrafı kullanılır
            ViewBag.FallbackImage = _context.heroSections.Select(x => x.MainImageUrl).FirstOrDefault();
            return View(about);
        }
    }
}
