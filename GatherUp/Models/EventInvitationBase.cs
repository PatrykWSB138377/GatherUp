using System.ComponentModel;

namespace GatherUp.Models
{
    public abstract class EventInvitationBase
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
        [DisplayName("Data wysłania")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Data odpowiedzi")]
        public DateTime? ResolvedDate { get; set; }
        [DisplayName("Status")]
        public InvitationStatus Status { get; set; }
    }

    public enum InvitationStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}
