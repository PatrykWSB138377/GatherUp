using GatherUp.Models;
using GatherUp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GatherUp.Views.Shared.Components.AcceptRequestButton
{
    public class AcceptOrRejectRequestButtonsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(EventJoinRequestViewModel joinRequestModel, string classes, bool reloadOnChange)
        {
            var viewModel = new AcceptOrRejectRequestButtonsViewModel
            {
                EventJoinRequest = joinRequestModel,
                Classes = classes,
                ReloadOnChange = reloadOnChange
            };

            return View(viewModel);
        }
    }
}
