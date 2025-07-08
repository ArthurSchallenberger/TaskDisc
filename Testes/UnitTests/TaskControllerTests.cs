using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskDisc.Application.Interfaces;
using TaskDisc.Core.Entities;
using TaskDisc.Presentation.Controllers;
using TaskDisc.Presentation.Dto;


namespace Testes.UnitTests;

public class TaskControllerTests
{
    private readonly Mock<ITaskService> _taskServiceMock;
    private readonly TaskController _taskController;

    public TaskControllerTests()
    {
        _taskServiceMock = new Mock<ITaskService>();
        _taskController = new TaskController(_taskServiceMock.Object);
    }

    [Fact]
    public async Task CreateTask_ValidTaskDto_ReturnsOkWithTask()
    {
        var taskDto = new TaskDto
        {
            Subject = "Test Task",
            Description = "Description",
            Priority = 1,
            Status = "Pending"
        };
        var taskEntity = new TaskEntity
        {
            Id = 1,
            Subject = "Test Task",
            Description = "Description",
            Priority = 1,
            Status = "Pending",
            Creation_Date = DateTime.UtcNow
        };
        
        _taskServiceMock.Setup(x => x.CreateTask(taskDto)).ReturnsAsync(taskEntity);
        var result = await _taskController.CreateTask(taskDto);
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal(taskEntity, okResult.Value);
    }

    [Fact]
    public async Task CreateTask_NullTaskDto_ReturnsBadRequest()
    {
        _taskServiceMock.Setup(x => x.CreateTask(null)).ReturnsAsync((TaskEntity?)null);
        var result = await _taskController.CreateTask(null);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        Assert.Equal("Failed to create task.", badRequestResult.Value);
    }

    [Fact]
    public async Task DeleteTask_ValidId_ReturnsTrue()
    {
        var id = 1;
        
        _taskServiceMock.Setup(x => x.DeleteTask(id)).ReturnsAsync(true);
        var result = await _taskController.DeleteTask(id);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteTask_InvalidId_ReturnsFalse()
    {
        var id = 999;
        
        _taskServiceMock.Setup(x => x.DeleteTask(id)).ReturnsAsync(false);
        var result = await _taskController.DeleteTask(id);

        Assert.False(result);
    }

    [Fact]
    public async Task UpdateTask_ValidTaskDto_ReturnsOkWithTask()
    {
        var id = 1;
        var taskDto = new TaskDto
        {
            Id = id,
            Subject = "Updated Task",
            Description = "Updated Description",
            Priority = 2,
            Status = "In Progress"
        };
        var taskEntity = new TaskEntity
        {
            Id = id,
            Subject = "Updated Task",
            Description = "Updated Description",
            Priority = 2,
            Status = "In Progress",
            Creation_Date = DateTime.UtcNow
        };
        
        _taskServiceMock.Setup(x => x.UpdateTask(taskDto)).ReturnsAsync(taskDto);
        var result = await _taskController.UpdateTask(id, taskDto);
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal(taskDto, okResult.Value);
    }

    [Fact]
    public async Task UpdateTask_TaskNotFound_ReturnsNotFound()
    {
        var id = 999;
        var taskDto = new TaskDto
        {
            Id = id,
            Subject = "Updated Task",
            Description = "Updated Description",
            Priority = 2,
            Status = "In Progress"
        };
        
        _taskServiceMock.Setup(x => x.UpdateTask(taskDto)).ReturnsAsync((TaskDto?)null);
        var result = await _taskController.UpdateTask(id, taskDto);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

        Assert.Equal($"Task with ID {id} not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task GetAll_TasksExist_ReturnsOkWithTasks()
    {
        var tasks = new List<TaskDto>
        {
            new TaskDto { Id = 1, Subject = "Task 1", Description = "Description 1", Priority = 1, Status = "Pending" },
            new TaskDto { Id = 2, Subject = "Task 2", Description = "Description 2", Priority = 2, Status = "Completed" }
        };

        _taskServiceMock.Setup(x => x.GetAllTasks()).ReturnsAsync(tasks);
        var result = await _taskController.GetAll();
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal(tasks, okResult.Value);
    }

    [Fact]
    public async Task GetAll_NoTasks_ReturnsNotFound()
    {
        _taskServiceMock.Setup(x => x.GetAllTasks()).ReturnsAsync((IEnumerable<TaskDto>?)null);
        var result = await _taskController.GetAll();
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

        Assert.Equal("No tasks found.", notFoundResult.Value);
    }

    [Fact]
    public async Task GetById_ValidId_ReturnsOkWithTask()
    {
        var id = 1;
        var taskDto = new TaskDto
        {
            Id = id,
            Subject = "Test Task",
            Description = "Description",
            Priority = 1,
            Status = "Pending"
        };
       
        _taskServiceMock.Setup(x => x.GetTaskById(id)).ReturnsAsync(taskDto);
        var result = await _taskController.GetById(id);
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal(taskDto, okResult.Value);
    }

    [Fact]
    public async Task GetById_InvalidId_ReturnsNotFound()
    {
        var id = 999;
       
        _taskServiceMock.Setup(x => x.GetTaskById(id)).ReturnsAsync((TaskDto?)null);
        var result = await _taskController.GetById(id);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

        Assert.Equal($"Task with ID {id} not found.", notFoundResult.Value);
    }
}