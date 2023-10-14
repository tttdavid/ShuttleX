using Microsoft.EntityFrameworkCore;
using SimpleChatApplication.Data;
using SimpleChatApplication.Hubs;
using SimpleChatApplication.Interfaces;
using SimpleChatApplication.Services;

namespace SimpleChatApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            DotNetEnv.Env.Load("../build/");

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddScoped<IChatService, ChatService>();
            builder.Services.AddScoped<IConnectionService, ConnectionService>();
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("connection"));
            });
            builder.Services.AddSignalR();

            var app = builder.Build();

            app.MapControllers();

            app.MapHub<ChatHub>("/chat");

            app.Run();
        }
    }
}