namespace ITI_Project.Models
{
    public class ChatBot
    {
        public int Id { get; set; }
        public string UserMessage { get; set; }
        public string BotReply { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
