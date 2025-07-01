using TaskDisc.Application.Interfaces;
using TaskDisc.Presentation.Controllers;
using TaskDisc.Presentation.Dto;
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
            var tasks = new List<TaskDto> { new TaskDto { Id = 1, Subject = "Test Task" } };

            _mockService.Setup(s => s.GetAllTasks()).ReturnsAsync(tasks);
            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnValue = Assert.IsType<List<TaskDto>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal("Test Task", returnValue[0].Subject);
        }
    }
}
