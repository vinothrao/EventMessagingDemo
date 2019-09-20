using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class SignalRNotificationHub : Hub
    {
        protected IHubContext<SignalRNotificationHub> _context;

        public SignalRNotificationHub(IHubContext<SignalRNotificationHub> context)
        {
            this._context = context;
        }


        public async Task ReceiveMessage(string message)
        {
            await  _context.Clients.All.SendAsync("ReceiveMessage",message);
        }
    }
}