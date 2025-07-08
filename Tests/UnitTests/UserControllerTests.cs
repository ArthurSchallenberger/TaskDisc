using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskDisc.Application.Interfaces;
using TaskDisc.Presentation.Controllers;
using TaskDisc.Presentation.Dto;
using TaskDisc.Core.Entities;

namespace Testes.UnitTests;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userServiceMock; 
    private readonly UserController _userController;
    public UserControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _userController = new UserController(_userServiceMock.Object);
    }

    [Fact]
    public async Task CreateUser_ValidUserDto_ReturnsOkWithUser()
    {
        var jobTitleDto = new JobTitlesDto { Id = 1 };
        var tokenDto = new TokenDto { Id = 1 };
        var userDto = new UserDto
        {
            Id = 1,
            Name = "Test User",
            Password = "password123",
            Email = "test@example.com",
            ID_JobTitle = 1,
            ID_Token = 1,
            JobTitle = new JobTitlesDto { Id = 1 },
            Tokens = new List<TokenDto> { tokenDto }
        };
        var userEntity = new UserEntity
        {
            Id = 1,
            Name = "Test User",
            Password = "hashedpassword",
            Email = "test@example.com",
            ID_JobTitle = 1,
            JobTitle = new JobTitlesEntity { Id = 1 },
            TaskUsers = new List<TaskUserEntity>()
        };
        _userServiceMock.Setup(x => x.CreateUser(userDto)).ReturnsAsync(userEntity);

        var result = await _userController.CreateUser(userDto);
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal(userEntity, okResult.Value);
    }

    [Fact]
    public async Task CreateUser_NullUserDto_ReturnsBadRequest()
    {
        _userServiceMock.Setup(x => x.CreateUser(null)).ReturnsAsync((UserEntity?)null);

        var result = await _userController.CreateUser(null);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        Assert.Equal("Failed to create user.", badRequestResult.Value);
    }

    [Fact]
    public async Task DeleteUser_ValidId_ReturnsOk()
    {
        var id = 1;
        _userServiceMock.Setup(x => x.DeleteUser(id)).ReturnsAsync(true);

        var result = await _userController.DeleteUser(id);
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal("User deleted successfully.", okResult.Value);
    }

    [Fact]
    public async Task DeleteUser_InvalidId_ReturnsNotFound()
    {
        var id = 999;
        _userServiceMock.Setup(x => x.DeleteUser(id)).ReturnsAsync(false);

        var result = await _userController.DeleteUser(id);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

        Assert.Equal($"User with ID {id} not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task UpdateUser_ValidUserDto_ReturnsOkWithUser()
    {
        var id = 1;
        var jobTitleDto = new JobTitlesDto { Id = 1 };
        var tokenDto = new TokenDto { Id = 1 };
        var userDto = new UserDto
        {
            Id = id,
            Name = "Updated User",
            Password = "newpassword123",
            Email = "updated@example.com",
            ID_JobTitle = 1,
            ID_Token = 1,
            JobTitle = jobTitleDto,
            Tokens = new List<TokenDto> { tokenDto }
        };
        var userEntity = new UserEntity
        {
            Id = id,
            Name = "Updated User",
            Password = "hashednewpassword",
            Email = "updated@example.com",
            ID_JobTitle = 1,
            JobTitle = new JobTitlesEntity { Id = 1 },
            TaskUsers = new List<TaskUserEntity>()
        };
       
        _userServiceMock.Setup(x => x.UpdateUser(userDto)).ReturnsAsync(userDto);
        var result = await _userController.UpdateUser(id, userDto);
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal(userDto, okResult.Value);
    }

    [Fact]
    public async Task UpdateUser_UserNotFound_ReturnsNotFound()
    {
        var id = 999;
        var jobTitleDto = new JobTitlesDto { Id = 999 };
        var tokenDto = new TokenDto { Id = 1 };
        var userDto = new UserDto
        {
            Id = id,
            Name = "Updated User",
            Password = "newpassword123",
            Email = "updated@example.com",
            ID_JobTitle = 999,
            ID_Token = 1,
            JobTitle = jobTitleDto,
            Tokens = new List<TokenDto> { tokenDto }
        };
        
        _userServiceMock.Setup(x => x.UpdateUser(userDto)).ReturnsAsync((UserDto?)null);
        var result = await _userController.UpdateUser(id, userDto);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

        Assert.Equal($"User with ID {id} not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task GetAllUsers_UsersExist_ReturnsOkWithUsers()
    {
        var jobTitleDto = new JobTitlesDto { Id = 1 };
        var tokenDto = new TokenDto { Id = 1 };
        var users = new List<UserDto>
    {
        new UserDto { Id = 1, Name = "User 1", Email = "user1@example.com", ID_JobTitle = 1, JobTitle = jobTitleDto, Tokens = new List<TokenDto> { tokenDto } },
        new UserDto { Id = 2, Name = "User 2", Email = "user2@example.com", ID_JobTitle = 1, JobTitle = jobTitleDto, Tokens = new List<TokenDto> { tokenDto } }
    };
        
        _userServiceMock.Setup(x => x.GetAllUsers()).ReturnsAsync(users);
        var result = await _userController.GetAllUsers();
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal(users, okResult.Value);
    }

    [Fact]
    public async Task GetAllUsers_NoUsers_ReturnsNotFound()
    {
        _userServiceMock.Setup(x => x.GetAllUsers()).ReturnsAsync((IEnumerable<UserDto>?)null);

        var result = await _userController.GetAllUsers();
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

        Assert.Equal("No users found.", notFoundResult.Value);
    }

    [Fact]
    public async Task GetUserById_ValidId_ReturnsOkWithUser()
    {
        var id = 1;
        var jobTitleDto = new JobTitlesDto { Id = 1 };
        var tokenDto = new TokenDto { Id = 1 };
        var userDto = new UserDto
        {
            Id = id,
            Name = "Test User",
            Email = "test@example.com",
            ID_JobTitle = 1,
            ID_Token = 1,
            JobTitle = jobTitleDto,
            Tokens = new List<TokenDto> { tokenDto }
        };
        
        _userServiceMock.Setup(x => x.GetUserById(id)).ReturnsAsync(userDto);
        var result = await _userController.GetUserById(id);
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal(userDto, okResult.Value);
    }

    [Fact]
    public async Task GetUserById_InvalidId_ReturnsNotFound()
    {
        var id = 999;
        
        _userServiceMock.Setup(x => x.GetUserById(id)).ReturnsAsync((UserDto?)null);
        var result = await _userController.GetUserById(id);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

        Assert.Equal($"User with ID {id} not found.", notFoundResult.Value);
    }
}

