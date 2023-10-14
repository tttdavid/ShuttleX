using Microsoft.EntityFrameworkCore;
using SimpleChatApplication.Data;

namespace SimpleChatApplication.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Chat> Chats { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Connection> LiveConnections { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}