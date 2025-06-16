using Api_Restful.Core.UseCases;
using Discord;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Api_Restful.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpPost]
        //public IActionResult CreateUser([FromBody] User user)
        //{
        //    if (user == null) return BadRequest("User data is required.");
        //    var createdUser = _userService.CreateUser(user);
        //    return CreatedAtAction(nameof(GetUser), new { id = createdUser.ID_PK }, createdUser);
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetUser(int id)
        //{
        //    var user = _userService.GetUserById(id);
        //    if (user == null) return NotFound();
        //    return Ok(user);
        //}   

        //[HttpPut("{id}")]
        //public IActionResult UpdateUser(int id, [FromBody] User user)
        //{
        //    if (user == null || id != user.ID_PK) return BadRequest("Invalid user data or ID mismatch.");
        //    var updatedUser = _userService.UpdateUser(user);
        //    if (updatedUser == null) return NotFound();
        //    return Ok(updatedUser);
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteUser(int id)
        //{
        //    var success = _userService.SoftDeleteUser(id);
        //    if (!success) return NotFound();
        //    return NoContent();
        //}
    }
}
