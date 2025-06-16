using Api_Restful.Core.UseCases;
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

        //[HttpPost]
        //public IActionResult CreateTask([FromBody] Task task)
        //{
        //    if (task == null) return BadRequest("Task data is required.");
        //    var createdTask = _taskService.CreateTask(task);
        //    return CreatedAtAction(nameof(GetTask), new { id = createdTask.ID_PK }, createdTask);
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetTask(int id)
        //{
        //    var task = _taskService.GetTaskById(id);
        //    if (task == null) return NotFound();
        //    return Ok(task);
        //}

        //[HttpGet]
        //public IActionResult GetTasksByUser([FromQuery] int assignedTo)
        //{
        //    var tasks = _taskService.GetTasksByUserId(assignedTo);
        //    return Ok(tasks);
        //}

        //[HttpPut("{id}")]
        //public IActionResult UpdateTask(int id, [FromBody] Task task)
        //{
        //    if (task == null || id != task.ID_PK) return BadRequest("Invalid task data or ID mismatch.");
        //    var updatedTask = _taskService.UpdateTask(task);
        //    if (updatedTask == null) return NotFound();
        //    return Ok(updatedTask);
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteTask(int id)
        //{
        //    var success = _taskService.DeleteTask(id);
        //    if (!success) return NotFound();
        //    return NoContent();
        //}
    }
}
