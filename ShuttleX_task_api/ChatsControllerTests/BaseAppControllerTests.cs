using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShuttleX_task_api.Controllers;
using ShuttleX_task_api.Models;
using ShuttleX_task_api.Services.Interfaces.DB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTestsController
{
    public abstract class BaseAppControllerTests<TEntity>
    {
        protected Mock<IBaseService<TEntity>> _mockService;
        protected BaseAppController<TEntity> _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IBaseService<TEntity>>();
            _controller = new Mock<BaseAppController<TEntity>>(_mockService.Object) { CallBase = true }.Object;
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsOk()
        {
            // Arrange
            var testData = new List<TEntity>
            {
                CreateTestEntity(),
                CreateTestEntity()
                // Add more test entities as needed
            };
            _mockService.Setup(x => x.GetAllAsync()).ReturnsAsync(testData);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            CollectionAssert.AreEqual(testData, (System.Collections.ICollection?)okResult.Value);
        }

        [TestMethod]
        public async Task GetByIdAsync_ExistingId_ReturnsOk()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var entity = CreateTestEntity();
            _mockService.Setup(x => x.GetByIdAsync(entityId)).ReturnsAsync(entity);

            // Act
            var result = await _controller.GetByIdAsync(entityId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(entity, okResult.Value);
        }

        [TestMethod]
        public async Task CreateAsync_ValidEntity_ReturnsCreated()
        {
            // Arrange
            var entity = CreateTestEntity();
            _mockService.Setup(x => x.AddAsync(entity)).ReturnsAsync(entity);

            // Act
            var result = await _controller.CreateAsync(entity);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(201, (result as ObjectResult).StatusCode);
            Assert.AreEqual(entity, (result as ObjectResult).Value);
        }

        [TestMethod]
        public async Task UpdateAsync_ExistingEntity_ReturnsNoContent()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var entity = CreateTestEntity();
            _mockService.Setup(x => x.UpdateAsync(entity));

            // Act
            var result = await _controller.UpdateAsync(entityId, entity);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        [TestMethod]
        public virtual async Task DeleteAsync_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            _mockService.Setup(x => x.DeleteAsync(entityId));

            // Act
            var result = await _controller.DeleteAsync(entityId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        // Abstract method for creating test entity, to be implemented in derived classes
        protected abstract TEntity CreateTestEntity();
    }
    
}
