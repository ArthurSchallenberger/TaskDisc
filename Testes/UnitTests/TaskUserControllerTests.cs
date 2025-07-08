using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskDisc.Application.Interfaces;
using TaskDisc.Core.Entities;
using TaskDisc.Presentation.Controllers;
using TaskDisc.Presentation.Dto;

namespace Testes.UnitTests;

public class TaskUserControllerTests
{
    private readonly Mock<ITaskUserService> _taskUserServiceMock; 
    private readonly TaskUserController _taskUserController;

    public TaskUserControllerTests()
    {
        _taskUserServiceMock = new Mock<ITaskUserService>();
        _taskUserController = new TaskUserController(_taskUserServiceMock.Object);
    }

    [Fact]
    public async Task CreateTaskUser_ValidTaskUserDto_ReturnsOkWithTaskUser()
    {
        var userDto = new UserDto { Id = 1 }; 
        var taskDto = new TaskDto { Id = 1 };
        var taskUserDto = new TaskUserDto
        {
            Id = 1,
            ID_User = 1,
            ID_Task = 1,
            User = userDto,
            Task = taskDto
        };
        var taskUserEntity = new TaskUserEntity
        {
            Id = 1,
            Id_User = 1,
            Id_Task = 1,
            User = new UserEntity { Id = 1 },
            Task = new TaskEntity { Id = 1 }  
        };
       
        _taskUserServiceMock.Setup(x => x.CreateTaskUser(taskUserDto)).ReturnsAsync(taskUserEntity);
        var result = await _taskUserController.CreateTaskUser(taskUserDto);
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal(taskUserEntity, okResult.Value);
    }

    [Fact]
    public async Task CreateTaskUser_NullTaskUserDto_ReturnsBadRequest()
    {
        _taskUserServiceMock.Setup(x => x.CreateTaskUser(null)).ReturnsAsync((TaskUserEntity?)null);
        var result = await _taskUserController.CreateTaskUser(null);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        Assert.Equal("Failed to create task-user association.", badRequestResult.Value);
    }

    [Fact]
    public async Task DeleteTaskUser_ValidId_ReturnsOk()
    {
        var id = 1;
        
        _taskUserServiceMock.Setup(x => x.DeleteTaskUser(id)).ReturnsAsync(true);
        var result = await _taskUserController.DeleteTaskUser(id);
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal("Task-User association deleted successfully.", okResult.Value);
    }

    [Fact]
    public async Task DeleteTaskUser_InvalidId_ReturnsNotFound()
    {
        var id = 999;
        
        _taskUserServiceMock.Setup(x => x.DeleteTaskUser(id)).ReturnsAsync(false);
        var result = await _taskUserController.DeleteTaskUser(id);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

        Assert.Equal($"Task-User association with ID {id} not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task UpdateTaskUser_ValidTaskUserDto_ReturnsOkWithTaskUser()
    {
        var id = 1;
        var userDto = new UserDto { Id = 1 };
        var taskDto = new TaskDto { Id = 2 };
        var taskUserDto = new TaskUserDto
        {
            Id = id,
            ID_User = id,
            ID_Task = 2,
            User = userDto,
            Task = taskDto
        };
        var taskUserEntity = new TaskUserEntity
        {
            Id = id,
            Id_User = id,
            Id_Task = 2,
            User = new UserEntity { Id = 1 },
            Task = new TaskEntity { Id = 2 }
        };

        _taskUserServiceMock.Setup(x => x.Update(taskUserDto)).ReturnsAsync(taskUserDto);
        var result = await _taskUserController.UpdateTaskUser(id, taskUserDto);
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal(taskUserDto, okResult.Value);
    }

    [Fact]
    public async Task UpdateTaskUser_TaskUserNotFound_ReturnsNotFound()
    {
        var id = 999;
        var userDto = new UserDto { Id = 999 };
        var taskDto = new TaskDto { Id = 2 };
        var taskUserDto = new TaskUserDto
        {
            Id = id,
            ID_User = id,
            ID_Task = 2,
            User = userDto,
            Task = taskDto
        };
       
        _taskUserServiceMock.Setup(x => x.Update(taskUserDto)).ReturnsAsync((TaskUserDto?)null);
        var result = await _taskUserController.UpdateTaskUser(id, taskUserDto);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal($"Task-User association with ID {id} not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task GetByTaskId_ValidTaskId_ReturnsOkWithTaskUser()
    {
        var taskId = 1;
        var userDto = new UserDto { Id = 1 };
        var taskDto = new TaskDto { Id = taskId };
        var taskUserDto = new TaskUserDto
        {
            Id = 1,
            ID_User = 1,
            ID_Task = taskId,
            User = userDto,
            Task = taskDto
        };
        
        _taskUserServiceMock.Setup(x => x.GetByTaskId(taskId)).ReturnsAsync(taskUserDto);
        var result = await _taskUserController.GetByTaskId(taskId);
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal(taskUserDto, okResult.Value);
    }

    [Fact]
    public async Task GetByTaskId_InvalidTaskId_ReturnsNotFound()
    {
        var taskId = 999;
        
        _taskUserServiceMock.Setup(x => x.GetByTaskId(taskId)).ReturnsAsync((TaskUserDto?)null);
        var result = await _taskUserController.GetByTaskId(taskId);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

        Assert.Equal($"No users found for task with ID {taskId}.", notFoundResult.Value);
    }
}