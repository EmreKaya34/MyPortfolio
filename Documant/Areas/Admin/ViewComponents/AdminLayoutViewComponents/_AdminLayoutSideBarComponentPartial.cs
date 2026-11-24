using Documant.Areas.Admin.Models;
using Documant.Context;
using Microsoft.AspNetCore.Mvc;

namespace Documant.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutSideBarComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _AdminLayoutSideBarComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var model = new AdminSidebarViewModel
            {
                OwnerName = _context.Abouts.Select(x => x.FullName).FirstOrDefault() ?? "Portfolyo",
                UnreadMessageCount = _context.messages.Count(x => !x.IsRead),
                CurrentController = ViewContext.RouteData.Values["controller"]?.ToString() ?? string.Empty
            };
            return View(model);
        }
    }
}
