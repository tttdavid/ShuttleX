using SimpleChatApplication.Data;
using SimpleChatApplication.Interfaces;

namespace SimpleChatApplication.Services
{
    public class MessageService : IMessageService
    {
        private readonly IApplicationDbContext _db;

        public MessageService(IApplicationDbContext context)
        {
            _db = context;
        }

        public async Task CreateMessage(int chatId, int userId, string Content)
        {
            _db.Messages.Add(new Message
            {
                ChatId = chatId,
                SenderId = userId,
                Content = Content
            });
            await _db.SaveChangesAsync();
        }
    }
}