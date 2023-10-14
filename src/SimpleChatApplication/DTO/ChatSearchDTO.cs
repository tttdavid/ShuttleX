using System.ComponentModel.DataAnnotations;

namespace SimpleChatApplication.DTO
{
    public class ChatSearchDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Chat title must contain only letters and numbers numbers")]
        public string Title { get; set; } = null!;
    }
}