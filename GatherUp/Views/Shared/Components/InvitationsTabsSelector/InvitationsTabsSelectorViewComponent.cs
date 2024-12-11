using Microsoft.AspNetCore.Mvc;

namespace GatherUp.Views.Shared.Components.InvitationsTabsSelector
{
    public class InvitationsTabsSelectorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string classes, string url, string buttonText)
        {
 
            return View();
        }
    }
}
