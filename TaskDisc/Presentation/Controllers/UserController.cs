﻿using TaskDisc.Application.Interfaces;
using TaskDisc.Core.Entities;
using TaskDisc.Presentation.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskDisc.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var user = await _userService.CreateUser(userDto);
            if (user is null)
            {
                return BadRequest("Failed to create user.");
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (!result)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok("User deleted successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            userDto.Id = id;
            var user = await _userService.UpdateUser(userDto);
            if (user is null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users is null || !users.Any())
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }

      
        [HttpGet("GetAllUsersIdAndName")]
        public async Task<IActionResult> GetAllUsersIdAndName()
        {
            var users = await _userService.GetAllUserIdAndName();
            if (users is null || !users.Any())
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user is null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }
    }
}
