using System.ComponentModel.DataAnnotations;

namespace SimpleChatApplication.DTO
{
    public class MessageDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Chat Id can't be negative or more then integer max value (2147483647)")]
        public int? ChatId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = $"Sender's Id can't be negative or more then integer max value (2147483647)")]
        public int? SenderId { get; set; }
        [Required]
        [RegularExpression(@"\S", ErrorMessage = "Invalid message content")]
        public string? Content { get; set; }
    }
}