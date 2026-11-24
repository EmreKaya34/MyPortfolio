using Microsoft.AspNetCore.Mvc;

namespace Documant.ViewCompanents.DefaultViewCompanents
{
    public class _DefaultHeaderComponentPartial:ViewComponent

    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
