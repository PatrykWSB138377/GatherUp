namespace GatherUp.Models.ViewModels
{
    public class AcceptOrRejectRequestButtonsViewModel
    {
        public string? Classes { get; set; }
        public bool? ReloadOnChange { get; set; }
        public EventJoinRequestViewModel EventJoinRequest { get; set; }
    }
}
