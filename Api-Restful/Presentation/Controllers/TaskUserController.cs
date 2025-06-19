using Api_Restful.Application.Interfaces;
using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api_Restful.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskUserController : ControllerBase
    {
        private readonly ITaskUserService _taskUserService;
        
        public TaskUserController(ITaskUserService taskUserService)
        {
            _taskUserService = taskUserService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskUser([FromBody] TaskUserDto taskUserDto)
        {
            var taskUser = await _taskUserService.CreateTaskUser(taskUserDto);
            if (taskUser is null)
            {
                return BadRequest("Failed to create task-user association.");
            }
            return Ok(taskUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTaskUser(int id)
        {
            var result = _taskUserService.DeleteTaskUser(id);
            if (!result)
            {
                return NotFound($"Task-User association with ID {id} not found.");
            }
            return Ok("Task-User association deleted successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskUser(int id, [FromBody] TaskUserDto taskUserDto)
        {
            taskUserDto.ID_User = id;
            var taskUser = await _taskUserService.Update(taskUserDto);
            if (taskUser is null)
            {
                return NotFound($"Task-User association with ID {id} not found.");
            }
            return Ok(taskUser);
        }

        [HttpGet]
        public async Task<IActionResult> GetByTaskId(int taskId)
        {
            var taskUser = await _taskUserService.GetByTaskId(taskId);
            if (taskUser is null)
            {
                return NotFound($"No users found for task with ID {taskId}.");
            }
            return Ok(taskUser);
        }
    }
}
