using System.ComponentModel.DataAnnotations;

namespace SimpleChatApplication.DTO
{
    public class CreateChatDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Authors Id can't be negative or more then integer max value (2147483647)")]
        public int? AuthorId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Chat title must contain only letters and numbers numbers")]
        public string Title  { get; set; } = null!;
    }
}