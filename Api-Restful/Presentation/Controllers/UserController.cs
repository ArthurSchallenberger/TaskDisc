using Api_Restful.Application.Interfaces;
using Api_Restful.Presentation.Dto;
using Discord;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Api_Restful.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<User> createUser(UserDto userDto)
        {
            return await _userService.CreateUser(userDto);
        }

        [HttpDelete("{id}")]
        public bool DeleteUser(int id)
        {
            return _userService.DeleteUser(id);
        }

        [HttpPut("{id}")]
        public async Task<User> UpdateUser(int id, UserDto userDto)
        {
            userDto.ID_PK = id; 
            return await _userService.UpdateUser(userDto);
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetUserById(int id)
        {
            return await _userService.GetUserById(id);
        }
    }
}
