using Documant.Context;
using Microsoft.AspNetCore.Mvc;

namespace Documant.ViewCompanents
{
    public class _DefaultNavbarComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultNavbarComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Marka adı Admin > Hakkımda ekranından gelir
            var ownerName = _context.Abouts.Select(x => x.FullName).FirstOrDefault() ?? "Portfolyo";
            return View("Default", ownerName);
        }
    }
}
