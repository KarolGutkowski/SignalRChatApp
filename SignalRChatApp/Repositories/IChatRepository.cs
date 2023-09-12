using SignalRChatApp.Models;

namespace SignalRChatApp.Repositories
{
    public interface IChatRepository
    {
        List<ChatMessage> getAllMessages();
        void addMessage(ChatMessage message);
    }
}
