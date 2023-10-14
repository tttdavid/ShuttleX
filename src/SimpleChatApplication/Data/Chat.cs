using System.ComponentModel.DataAnnotations;

namespace SimpleChatApplication.Data
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; } = "Noname chat";
        public List<Message> Messages { get; set; } = new();
    }
}