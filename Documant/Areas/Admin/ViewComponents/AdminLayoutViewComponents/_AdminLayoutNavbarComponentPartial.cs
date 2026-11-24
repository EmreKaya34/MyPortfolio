using Documant.Context;
using Microsoft.AspNetCore.Mvc;

namespace Documant.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutNavbarComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _AdminLayoutNavbarComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Okunmamış mesaj sayısı zil ikonundaki bildirim noktası için kullanılır
            return View(_context.messages.Count(x => !x.IsRead));
        }
    }
}
