namespace GatherUp.Models.ViewModels
{
    public class EventViewModel : Event
    {
        public string UserName { get; set; } 
        public bool? IsUserEvent { get; set; }
    }
}
