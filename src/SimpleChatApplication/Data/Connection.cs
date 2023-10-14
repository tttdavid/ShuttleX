using System.ComponentModel.DataAnnotations;

namespace SimpleChatApplication.Data
{
    public class Connection
    {
        [Key]
        public int Id { get; set; }
        public string ConnectionId { get; set; } = "Undefined";
        public int CurrentChatId { get; set; }
        public int UserId { get; set; }
    }
}