using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleChatApplication.Data
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ChatId")]
        public int ChatId { get; set; }
        public int SenderId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Content { get; set; } = "Undefined";
    }
}