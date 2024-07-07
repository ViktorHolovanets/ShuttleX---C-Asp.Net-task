using Microsoft.AspNetCore.Mvc;
using Moq;
using ShuttleX_task_api.Controllers;
using ShuttleX_task_api.Helpers.Classes;
using ShuttleX_task_api.Models;
using ShuttleX_task_api.Services.Interfaces.DB;


namespace UnitTestsController
{
    [TestClass]
    public class ChatControllerTests : BaseAppControllerTests<Chat>
    {
        private Guid _userId;

        [TestInitialize]
        public new void Setup()
        {
            _userId = Guid.NewGuid();
            _mockService = new Mock<IBaseService<Chat>>();
            _controller = new ChatController(_mockService.Object);
        }

        protected override Chat CreateTestEntity()
        {
            return new Chat { Id = Guid.NewGuid(), Name = "TestChat", CreatedByUserId = _userId };
        }

        [TestMethod]
        public override async Task DeleteAsync_ExistingId_ReturnsNoContent()
        {
            var entityId = Guid.NewGuid();
            _mockService.Setup(x => x.DeleteAsync(entityId));

            var result = await _controller.DeleteAsync(entityId);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual("This method is not available.", (result as BadRequestObjectResult).Value);
        }

        [TestMethod]
        public async Task DeleteAsyncWithRequest_ExistingIdAndValidRequest_ReturnsNoContent()
        {
            var entityId = Guid.NewGuid();
            var request = new DeleteChatRequest { UserId = _userId };
            var chat = new Chat { Id = entityId, CreatedByUserId = _userId, Name = "TestChat" };

            _mockService.Setup(x => x.GetByIdAsync(entityId)).ReturnsAsync(chat);

            var result = await (_controller as ChatController).DeleteAsyncWithRequest(entityId, request);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteAsyncWithRequest_ExistingIdAndInvalidRequest_ReturnsForbid()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var request = new DeleteChatRequest { UserId = Guid.NewGuid() };
            var chat = new Chat { Id = entityId, CreatedByUserId = _userId, Name = "TestChat" };

            _mockService.Setup(x => x.GetByIdAsync(entityId)).ReturnsAsync(chat);

            // Act
            var result = await (_controller as ChatController).DeleteAsyncWithRequest(entityId, request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ForbidResult));
        }

    }
}
