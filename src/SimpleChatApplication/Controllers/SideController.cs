using Microsoft.AspNetCore.Mvc;
using SimpleChatApplication.DTO;
using SimpleChatApplication.Interfaces;

namespace SimpleChatApplication.Controllers
{
    [ApiController]
    public class SideController : ControllerBase
    {
        private readonly IChatService _chatService;

        public SideController(IChatService chatService) => _chatService = chatService;

        [HttpPost("/api/chats/search")]
        public IActionResult FindChatByTitle([FromBody] ChatSearchDTO options) => _chatService.FindChatByTitle(options);

        [HttpGet("/api/chats/{chatId=int}")]
        public IActionResult GetChatInfo(int chatId) => _chatService.GetChatInfo(chatId);
    }
}