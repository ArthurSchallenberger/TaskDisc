using TaskDisc.Application.Interfaces;
using TaskDisc.Presentation.Dto;
using Microsoft.AspNetCore.Mvc;

namespace TaskDisc.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] LoginDto request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required.");
            }


            string token = await _authService.AuthenticateToken(request.Email, request.Password);

            return Ok(new { token });
        }
        [HttpGet("{token}")]
        public async Task<IActionResult> ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }
            bool isValid = await _authService.ValidateToken(token);
            if (!isValid)
            {
                return Unauthorized("Invalid token.");
            }
            return Ok("Token is valid.");
        }
    }

   
}
