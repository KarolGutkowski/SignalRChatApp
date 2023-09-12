using SignalRChatApp.Models;

namespace SignalRChatApp.Repositories
{
    public class ChatRepository: IChatRepository
    {
        private List<ChatMessage> _messages { get; set; } = new List<ChatMessage>()
        {
            new ChatMessage("testUser", "test Message")
        };

        public void addMessage(ChatMessage message)
        {
            _messages.Add(message);
        }

        public List<ChatMessage> getAllMessages()
        {
            return _messages;
        }
    }
}
