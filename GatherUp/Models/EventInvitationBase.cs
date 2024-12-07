namespace GatherUp.Models
{
    public abstract class EventInvitationBase
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ResolvedDate { get; set; }

        public InvitationStatus Status { get; set; }
    }

    public enum InvitationStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}
