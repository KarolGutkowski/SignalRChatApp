using SignalRChatApp.Models;

namespace SignalRChatApp.Repositories
{
    public class ChatRepository: IChatRepository
    {
        private Dictionary<int, IEnumerable<ChatMessage>> _chatRooms =
            new Dictionary<int, IEnumerable<ChatMessage>>()
            {
                { 1, new List<ChatMessage>(){new ChatMessage("testUser", "test Message")}}
            };
        private List<ChatMessage> _messages { get; set; } = new List<ChatMessage>()
        {
            new ChatMessage("testUser", "test Message"),
            new ChatMessage("messge numero 2", "no. 2")
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
