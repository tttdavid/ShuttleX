using Microsoft.EntityFrameworkCore;
using SimpleChatApplication.Data;
using SimpleChatApplication.Interfaces;

namespace SimpleChatApplication.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IApplicationDbContext _db;

        public ConnectionService(IApplicationDbContext context)
        {
            _db = context;
        }

        public async Task CreateConnection(string con)
        {
            _db.LiveConnections.Add(new Connection
            {
                ConnectionId = con
            });
            await _db.SaveChangesAsync();
        }

        public async Task RemoveConnection(string con)
        {
            Connection? connection = _db.LiveConnections.FirstOrDefault(c => c.ConnectionId == con);
            if (connection != null)
            {
                _db.LiveConnections.Remove(connection!);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> IsConnectionNull(string con)
        {
            Connection? connection = await _db.LiveConnections.FirstOrDefaultAsync(lc => lc.ConnectionId == con);
            return connection == null;
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

        public async Task<bool> CheckUserConnection(string con, int userId)
        {
            return await _db.LiveConnections.AnyAsync(lc => lc.ConnectionId != con && lc.UserId == userId);
        }

        public async Task ModifyConnection(Connection connection, int chatId, int userId)
        {
            connection.CurrentChatId = chatId;
            connection.UserId = userId;

            _db.LiveConnections.Update(connection);
            await _db.SaveChangesAsync();
        }

        public async Task<Connection> GetConnection(string con)
        {
            Connection? connection = await _db.LiveConnections.FirstOrDefaultAsync(lc => lc.ConnectionId == con);
            return connection!;
        }
    }
}