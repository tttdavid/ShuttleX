using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SimpleChatApplication.Data;
using SimpleChatApplication.Interfaces;
using SimpleChatApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApplication.Test.Services
{
    public class MessageServiceTest
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
        public void MessageService_CreateMessage_Return()
        {
            // Arrange
            var chatId = 1;
            var senderId = 1;
            var Content = "Test message";
            var dbContext = GetDbContext();
            var messageService = new MessageService(dbContext);

            // Action
            var result = messageService.CreateMessage(chatId, senderId, Content);

            // Assert

        }
    }
}
