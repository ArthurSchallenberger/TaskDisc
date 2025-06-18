using Api_Restful.Application.Interfaces;
using Api_Restful.Presentation.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api_Restful.Presentation.Controllers
{
    public class TaskUserController 
    {
        private readonly ITaskUserService _taskUserService;
        public TaskUserController(ITaskUserService taskUserService)
        {
            _taskUserService = taskUserService;
        }
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            var taskUser = await _taskUserService.GetByUserId(id);
            if (taskUser == null)
            {
                return BadRequest("User not found or no tasks assigned.");
            }
            return Ok(taskUser);
        }

        [HttpGet("task/{taskId}")]
        public async Task<TaskUserDto> GetByTaskId(int taskId)
        {
            var taskUser = await _taskUserService.GetByTaskId(taskId);
            if (taskUser == null)
            {
               
            }
            
            return taskUser; 
        }
        [HttpPut]
        public async Task<IActionResult> Update(TaskUser taskUser)
        {
            var updatedTaskUser = await _taskUserService.Update(taskUser);
            if (updatedTaskUser == null)
            {
                return BadRequest("Update failed");
            }
            return Ok(updatedTaskUser);
        }

        private IActionResult Ok(TaskUser updatedTaskUser)
        {
            throw new NotImplementedException();
        }

        private IActionResult BadRequest(string v)
        {
            throw new NotImplementedException();
        }
    }
}
