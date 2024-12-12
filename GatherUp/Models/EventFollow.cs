using Microsoft.AspNetCore.Identity;

namespace GatherUp.Models
{
    public class EventFollow
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
        public int? EventId { get; set; }
        public Event? Event { get; set; }
    }
}
