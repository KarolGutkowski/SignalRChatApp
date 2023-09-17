using Microsoft.AspNetCore.SignalR;
using SignalRChatApp.Models;
using SignalRChatApp.Repositories;

namespace SignalRChatApp.Hubs
{
    public class ChatHub: Hub
    {
        private readonly IChatRepository _chatRepository;

        public ChatHub(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task NotifyNewMessage(ChatMessage message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", message);
        }

        public async Task NotifyThatImWriting()
        {
            
            await Clients.Others.SendAsync("ReceiveSomeoneWriting");
        }

        public override async Task OnConnectedAsync()
        {
            var allMessages = _chatRepository.getAllMessages();
            await Clients.Caller.SendAsync("GetAllMessages", allMessages);
            await base.OnConnectedAsync();
        }
    }
}
