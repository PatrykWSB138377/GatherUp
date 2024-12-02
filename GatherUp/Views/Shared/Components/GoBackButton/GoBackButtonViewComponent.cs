using Microsoft.AspNetCore.Mvc;

namespace GatherUp.Views.Shared.Components.GoBackButton
{

    public class GoBackButtonViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(string classes, string url, string buttonText)
        {
            ViewData["Classes"] = classes;
            ViewData["Url"] = url;
            ViewData["ButtonText"] = buttonText;

            return View();
        }
    }
}


