using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleChatApplication.Data;
using SimpleChatApplication.DTO;
using Microsoft.AspNetCore.SignalR;
using SimpleChatApplication.Hubs;
using SimpleChatApplication.Interfaces;

namespace SimpleChatApplication.Services
{
    public class ChatService : IChatService
    {
        private readonly IApplicationDbContext _db;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatService(IApplicationDbContext dbContext, IHubContext<ChatHub> hubContext)
        {
            _db = dbContext;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> GetAllChats()
        {
            if (_db.Chats.Any())
                return new JsonResult(await _db.Chats.ToListAsync());
            else
                return new NotFoundObjectResult(new { messag = $"There is not chats at this moment" });
        }

        public async Task<bool> IsChatNull(int chatId)
        {
            Chat? chat = await _db.Chats.FirstOrDefaultAsync(c => c.Id == chatId);
            return chat == null;
        }

        public async Task<IActionResult> CreateChat(CreateChatDto chat)
        {
            _db.Chats.Add(new Chat
            {
                AuthorId = (int)chat.AuthorId!,
                Title = chat.Title
            });
            await _db.SaveChangesAsync();

            return new OkObjectResult(new { messag = "Chat successfully created" });
        }

        public async Task<IActionResult> DeleteChat(DeleteChatDTO helper)
        {
            Chat? chat = await _db.Chats.FirstOrDefaultAsync(chat => chat.Id == helper.ChatId);

            if (chat == null)
                return new NotFoundObjectResult(new { messag = $"Chat with id {helper.ChatId} not found" });

            if (chat.AuthorId != helper.SenderId)
                return new BadRequestObjectResult(new { messag = $"User don't have permissions for this action" });

            await DisconnectAllUsersFromChatAsync(helper.ChatId);

            _db.Chats.Remove(chat);
            await _db.SaveChangesAsync();

            return new OkObjectResult(new { messag = "Chat successfully deleted" });
        }

        public IActionResult FindChatByTitle(ChatSearchDTO options)
        {
            var chats = _db.Chats.Where(chat => chat.Title.Contains(options.Title));
            if (chats.Any())
                return new JsonResult(chats.ToList());
            else
                return new NotFoundObjectResult(new { messag = "Chats withs this title not found" });
        }

        public IActionResult GetChatInfo(int chatId)
        {
            var chat = _db.Chats.Include(c => c.Messages).FirstOrDefault(c => c.Id == chatId);
            if (chat != null)
                return new JsonResult(chat);
            else
                return new NotFoundObjectResult(new { messag = $"Chat with id {chatId} not found" });
        }

        public async Task<Chat> GetChatWithHistory(int chatId)
        {
            Chat? chat = await _db.Chats.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == chatId);
            return chat!;
        }

        private async Task DisconnectAllUsersFromChatAsync(int? chatId)
        {
            var chatUsers = _db.LiveConnections.Where(lc => lc.CurrentChatId == chatId);
            await _hubContext.Clients.Group(chatId.ToString()!).SendAsync("ReciveMEssage", "Chat is deleted by owner, disconecting...");
            foreach (Connection con in chatUsers)
            {
                await _hubContext.Groups.RemoveFromGroupAsync(con.ConnectionId, chatId.ToString()!);
            }
        }
    }
}