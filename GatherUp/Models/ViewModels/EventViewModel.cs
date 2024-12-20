﻿namespace GatherUp.Models.ViewModels
{
    public class EventViewModel : Event
    {
        public string? UserName { get; set; }
        public bool? IsUserEvent { get; set; }
        public EventFollow? UserFollow { get; set; }
        public EventJoinRequest? UserJoinRequest { get; set; }
        public List<string>? Participants { get; set; }
        public List<string>? Followers { get; set; }
    }
}
