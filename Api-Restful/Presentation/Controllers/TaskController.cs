using Api_Restful.Application.Interfaces;
using Api_Restful.Presentation.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api_Restful.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController 
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<Task> CreateTask(TaskDto taskDto)
        {
            return await _taskService.CreateTask(taskDto);
        }

        [HttpDelete("{id}")]
        public bool DeleteTask(int id)
        {
            return _taskService.DeleteTask(id);
        }

        [HttpPut("{id}")]
        public async Task<Task> UpdateTask(int id, TaskDto taskDto)
        {
            taskDto.ID_PK = id; 
            return await _taskService.UpdateTask(taskDto);
        }
    }
}

