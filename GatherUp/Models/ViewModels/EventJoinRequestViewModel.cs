using System.ComponentModel;

namespace GatherUp.Models.ViewModels
{
    public class EventJoinRequestViewModel : EventInvitationBase
    {
        [DisplayName("Użytkownik organizujący wydarzenie")]
        public string ReceiverUsername { get; set; }
        [DisplayName("Użytkownik proszący o przyjęcie do wydarzenia")]
        public string SenderUsername { get; set; }
    }
}
