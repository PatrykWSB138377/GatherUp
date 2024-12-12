using GatherUp.Models;
using GatherUp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GatherUp.Views.Shared.Components.FollowButton
{
    public class FollowButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string classes, bool reloadOnChange, EventViewModel eventModel)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = new FollowButtonViewModel
            {
                Event = eventModel,
                UserId = userId,
                Classes = classes,
                ReloadOnChange = reloadOnChange,
            };

            return View(model);
        }
    }
}
