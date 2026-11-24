using Documant.Context;
using Microsoft.AspNetCore.Mvc;

namespace Documant.ViewCompanents.DefaultViewCompanents
{
    public class _DefaulStyleComponentPartial: ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaulStyleComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Tarayıcı sekmesindeki başlık Admin > Hakkımda ekranından gelir
            var about = _context.Abouts.FirstOrDefault();
            return View("Default", about == null ? "Portfolyo" : $"{about.FullName} — {about.JobTitle}");
        }
    }
}
