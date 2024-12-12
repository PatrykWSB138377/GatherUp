namespace GatherUp.Models.ViewModels
{
    public class FollowButtonViewModel
    {
        public string Classes { get; set; }
        public EventViewModel Event { get; set; }
        public string UserId { get; set; }
        public bool ReloadOnChange { get; set; }
    }
}
