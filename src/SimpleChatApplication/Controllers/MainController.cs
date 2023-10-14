using Microsoft.AspNetCore.Mvc;
using SimpleChatApplication.DTO;
using SimpleChatApplication.Interfaces;

namespace SimpleChatApplication.Controllers
{
    [Route("api/chats")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IChatService _chatService;

        public MainController(IChatService chatService) => _chatService = chatService;

        [HttpGet]
        public async Task<IActionResult> GetAllChats() => await _chatService.GetAllChats();

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatDto chat) => await _chatService.CreateChat(chat);

        [HttpDelete]
        public async Task<IActionResult> DeleteChat([FromBody] DeleteChatDTO helper) => await _chatService.DeleteChat(helper);
    }
}