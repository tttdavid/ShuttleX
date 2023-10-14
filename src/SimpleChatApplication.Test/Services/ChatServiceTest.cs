using Microsoft.EntityFrameworkCore;
using SimpleChatApplication.Services;
using SimpleChatApplication.Data;
using SimpleChatApplication.Interfaces;
using Microsoft.AspNetCore.SignalR;
using SimpleChatApplication.Hubs;
using Microsoft.EntityFrameworkCore.InMemory;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace SimpleChatApplication.Test.Services
{
    public class ChatServiceTest
    {
        private ApplicationDbContext GetDbContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

            var context = new ApplicationDbContext(dbContextOptions);

            return context;
        }

        [Fact]
        public async void ChatService_GetAllChats_ReturnJsonResult()
        {
            // Arrange
            var dbContext = GetDbContext();
            dbContext.Chats.Add(new Chat { AuthorId = 1, Title = "Test chat" });
            await dbContext.SaveChangesAsync();
            var chatService = new ChatService(dbContext, A.Fake<IHubContext<ChatHub>>());

            // Action
            var result = await chatService.GetAllChats();

            // Assert
            result.Should().BeOfType<JsonResult>();
            dbContext.Chats.Should().HaveCount(1);
            dbContext.Chats.First().Should().BeOfType<Chat>();
        }
    }
}
