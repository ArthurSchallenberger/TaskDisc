using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskDisc.Application.Interfaces;
using TaskDisc.Presentation.Controllers;
using TaskDisc.Presentation.Dto;

namespace TaskDisc.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task GenerateToken_ValidCredentials_ReturnsOkWithToken()
        {
            var loginDto = new LoginDto { Email = "test@example.com", Password = "password" };
            _authServiceMock.Setup(x => x.AuthenticateToken(loginDto.Email, loginDto.Password))
                .ReturnsAsync("valid_token");

            var result = await _authController.GenerateToken(loginDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;
            Assert.NotNull(returnValue);
            var tokenProperty = returnValue.GetType().GetProperty("token")?.GetValue(returnValue);
            Assert.Equal("valid_token", tokenProperty);
        }

        [Fact]
        public async Task GenerateToken_InvalidCredentials_ReturnsBadRequest()
        {
            var loginDto = new LoginDto { Email = "", Password = "" };
            var result = await _authController.GenerateToken(loginDto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Username and password are required.", badRequestResult.Value);
        }

        [Fact]
        public async Task ValidateToken_ValidToken_ReturnsOk()
        {
            var token = "valid_token";
            
            _authServiceMock.Setup(x => x.ValidateToken(token)).ReturnsAsync(true);
            var result = await _authController.ValidateToken(token);
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal("Token is valid.", okResult.Value);
        }

        [Fact]
        public async Task ValidateToken_InvalidToken_ReturnsUnauthorized()
        {
            var token = "invalid_token";
            
            _authServiceMock.Setup(x => x.ValidateToken(token)).ReturnsAsync(false);
            var result = await _authController.ValidateToken(token);
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);

            Assert.Equal("Invalid token.", unauthorizedResult.Value);
        }
    }
}
