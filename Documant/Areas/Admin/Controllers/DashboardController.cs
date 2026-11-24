using Documant.Areas.Admin.Models;
using Documant.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Documant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly PortfolioContext _context;

        public DashboardController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                TotalProjectCount = _context.Projects.Count(),
                ActiveProjectCount = _context.Projects.Count(x => x.IsActive),
                SkillCount = _context.skills.Count(),
                ExperienceCount = _context.experiences.Count(),
                EducationCount = _context.Educations.Count(),
                TotalMessageCount = _context.messages.Count(),
                UnreadMessageCount = _context.messages.Count(x => !x.IsRead),

                // Son 5 mesaj
                LastMessages = _context.messages
                    .OrderByDescending(x => x.SendDate)
                    .Take(5)
                    .ToList(),

                // Son 3 proje (kategori adı için Include)
                LastProjects = _context.Projects
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.CreatedDate)
                    .Take(3)
                    .ToList(),

                CategorySummaries = GetCategorySummaries()
            };

            return View(model);
        }

        // Kategori özeti: GroupBy ile sayılır, projesi olmayan kategoriler 0 olarak eklenir
        private List<CategoryProjectCount> GetCategorySummaries()
        {
            var projectCounts = _context.Projects
                .GroupBy(x => x.CategoryId)
                .Select(g => new { CategoryId = g.Key, Count = g.Count() })
                .ToDictionary(x => x.CategoryId, x => x.Count);

            return _context.Categories
                .ToList()
                .Select(c => new CategoryProjectCount
                {
                    CategoryName = c.CategoryName,
                    ProjectCount = projectCounts.GetValueOrDefault(c.CategoryId)
                })
                .OrderByDescending(x => x.ProjectCount)
                .ThenBy(x => x.CategoryName)
                .ToList();
        }
    }
}
