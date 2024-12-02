using Microsoft.AspNetCore.Mvc;

namespace GatherUp.Views.Shared.Components.FollowButton
{
    public class FollowButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string classes)
        {
            ViewData["Classes"] = classes;

            return View();
        }
    }
}
