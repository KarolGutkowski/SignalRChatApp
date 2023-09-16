using Microsoft.AspNetCore.SignalR;
using SignalRChatApp.Models;

namespace SignalRChatApp.Hubs
{
    public class ChatHub: Hub
    {
        public async Task NotifyNewMessage(ChatMessage message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", message);
        }

        public async Task NotifyThatImWriting()
        {
            await Clients.Others.SendAsync("ReceiveSomeoneWriting");
        }
    }
}
