using Documant.Context;
using Documant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Documant.ViewCompanents.DefaultViewCompanents
{
    public class _DefaultProjectComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultProjectComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Pasif projeler UI de listelenmez; varsayılan sıralama: en yeni proje üstte
            var values = _context.Projects
                .Include(x => x.Category)
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();
            return View(values);
        }
    }
}
