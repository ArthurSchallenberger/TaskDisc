using Api_Restful.Application.Interfaces;
using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Testes.UnitTests
{
    public class TaskControllerTests
    {
        private readonly Mock<ITaskService> _mockService;
        private readonly TaskController _controller;

        public TaskControllerTests()
        {
            _mockService = new Mock<ITaskService>();
            _controller = new TaskController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            var tasks = new List<TaskEntity> { new TaskEntity { ID_PK = 1, Subject = "Test Task" } };
            _mockService.Setup(s => s.GetAllTasks()).Returns(tasks);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<TaskEntity>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal("Test Task", returnValue[0].Subject);
        }
    }
}
