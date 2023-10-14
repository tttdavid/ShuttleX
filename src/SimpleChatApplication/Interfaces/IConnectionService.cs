using SimpleChatApplication.Data;

namespace SimpleChatApplication.Interfaces
{
    public interface IConnectionService
    {
        Task CreateConnection(string con);
        Task RemoveConnection(string con);
        Task<bool> IsConnectionNull(string con);
        Task<bool> CheckUserConnection(string con, int userId);
        Task ModifyConnection(Connection connection, int chatId, int userId);
        Task<Connection> GetConnection(string con);
    }
}