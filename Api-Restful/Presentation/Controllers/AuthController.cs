using Api_Restful.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_Restful.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationService _authService;

        public AuthController(IJwtAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required.");
            }

            
            string userId = "user123"; 
            string role = "user"; 
            string token = _authService.GenerateToken(userId, role);

            return Ok(new { token });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
