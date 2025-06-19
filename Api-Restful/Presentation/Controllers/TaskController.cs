using Api_Restful.Application.Interfaces;
using Api_Restful.Presentation.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api_Restful.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto taskDto)
        {
            var task = await _taskService.CreateTask(taskDto);
            if (task is null)
            {
                return BadRequest("Failed to create task.");
            }

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteTask(int id)
        {
            return await _taskService.DeleteTask(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDto taskDto)
        {
            taskDto.ID_PK = id;
            var task = await _taskService.UpdateTask(taskDto);

            if (task is null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            return Ok(task);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllTasks();

            if (tasks is null)
            {
                return NotFound("No tasks found.");
            }

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetTaskById(id);
            if (task is null)
            {
                return NotFound($"Task with ID {id} not found.");
            }
            return Ok(task);
        }
    }
}

