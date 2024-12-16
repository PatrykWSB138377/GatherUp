using Microsoft.AspNetCore.SignalR;

namespace GatherUp
{
    public class MessagesManager : Hub
    {

        private readonly IHubContext<MessagesManager> _hubContext;


        public MessagesManager(IHubContext<MessagesManager> hubContext)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task EmitMessageForUsers(List<string> usersIds, string message)
        {

            if (_hubContext == null) 
            { 
                throw new InvalidOperationException("HubContext is not initialized.");
            }

            await Clients.Users(usersIds).SendAsync("ReceiveMessage", message);
        }
    }
}
