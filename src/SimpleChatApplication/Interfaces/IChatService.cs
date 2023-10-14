using Microsoft.AspNetCore.Mvc;
using SimpleChatApplication.Data;
using SimpleChatApplication.DTO;

namespace SimpleChatApplication.Interfaces
{
    public interface IChatService
    {
        Task<IActionResult> GetAllChats();
        Task<bool> IsChatNull(int chatId);
        Task<IActionResult> CreateChat(CreateChatDto chat);
        Task<IActionResult> DeleteChat(DeleteChatDTO helper);
        IActionResult FindChatByTitle(ChatSearchDTO options);
        IActionResult GetChatInfo(int chatId);
        Task<Chat> GetChatWithHistory(int chatId);
    }
}