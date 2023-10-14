namespace SimpleChatApplication.Interfaces
{
    public interface IMessageService
    {
        Task CreateMessage(int chatId, int userId, string Content);
    }
}