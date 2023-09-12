namespace SignalRChatApp.Models
{
    public class ChatMessage
    {
        public ChatMessage(string userName, string message)
        {
            this.userName = userName;
            this.message = message;
        }

        public string userName { get; set; }
        public string message { get; set; }
    }
}