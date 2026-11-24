using Microsoft.AspNetCore.Mvc;

namespace Documant.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutScriptComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
