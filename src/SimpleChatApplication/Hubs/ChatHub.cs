using Microsoft.AspNetCore.SignalR;
using SimpleChatApplication.Data;
using SimpleChatApplication.Interfaces;

namespace SimpleChatApplication.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IConnectionService _connectionService;
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;

        public ChatHub(IConnectionService connectionService, IChatService chatService, IMessageService messageService)
        {
            _connectionService = connectionService;
            _chatService = chatService;
            _messageService = messageService;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await _connectionService.CreateConnection(Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _connectionService.RemoveConnection(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinChat(int chatId, int userId)
        {
            if (await _chatService.IsChatNull(chatId))
            {
                await Clients.Caller.SendAsync("ReciveMessage", "Chat with this id doesn't exist");
                return;
            }

            if (await _connectionService.IsConnectionNull(Context.ConnectionId))
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "Test");
                return;
            }

            Connection con = await _connectionService.GetConnection(Context.ConnectionId);

            if (await _connectionService.CheckUserConnection(Context.ConnectionId, userId))
            {
                await Clients.Caller.SendAsync("ReciveMessage", "This user is already connected");
                return;
            }

            await Groups.AddToGroupAsync(con!.ConnectionId, chatId.ToString());
            await _connectionService.ModifyConnection(con, chatId, userId);

            await Clients.Caller.SendAsync("SendChatWithHistory", await _chatService.GetChatWithHistory(chatId));
        }

        public async Task SendMessageToChat(int chatId, int userId, string message)
        {
            if (await _chatService.IsChatNull(chatId))
            {
                await Clients.Caller.SendAsync("ReciveMessage", "Chat with this id doesn't exist");
                return;
            }

            if (await _connectionService.IsConnectionNull(Context.ConnectionId))
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "This user is not connected to chat");
                return;
            }

            Connection con = await _connectionService.GetConnection(Context.ConnectionId);

            if (con!.UserId != userId)
            {
                await Clients.Caller.SendAsync("ReciveMessage", "This user can't post to this chat");
                return;
            }
            else if (con!.CurrentChatId != chatId)
            {
                await Clients.Caller.SendAsync("ReciveMessage", "This user is not connected to this chat");
                return;
            }

            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", $"User-{userId}: {message}");
            await _messageService.CreateMessage(chatId, userId, message);
        }
    }
}