using Api_Restful.Application.Interfaces;
using Api_Restful.Presentation.Dto;
using Api_Restful.Core.Entities;


namespace Api_Restful.Application.Services
{
    public class TaskManagementService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskManagementService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskEntity> CreateTask(TaskDto taskDto)
        {
            if (taskDto == null)
            {
                throw new ArgumentNullException(nameof(taskDto), "Task data cannot be null.");
            }

            if (string.IsNullOrEmpty(taskDto.Description))
            {
                throw new ArgumentException("Description is mandatory.", nameof(taskDto.Description));
            }

            var task = new TaskEntity
            {
                ID_PK = taskDto.ID_PK,
                Description = taskDto.Description,
                Status = taskDto.Status,
                Creation_Date = DateTime.UtcNow,
                Priority = taskDto.Priority ?? 0,
                Subject = taskDto.Subject ?? "Sem Assunto",
              
            };

            return _taskRepository.Add(task);
        }

        public TaskEntity GetTaskById(int id)
        {
            return _taskRepository.GetById(id);
        }


        public async Task<TaskEntity> UpdateTask(TaskDto taskDto)
        {
            if (taskDto == null) throw new ArgumentNullException(nameof(taskDto), "Task data cannot be null.");
            var existingTask = _taskRepository.GetById(taskDto.ID_PK);
            if (existingTask == null) throw new InvalidOperationException("Task not found.");

            
            existingTask.Subject = taskDto.Subject ?? existingTask.Subject;
            existingTask.Description = taskDto.Description ?? existingTask.Description;
            existingTask.Status = taskDto.Status ?? existingTask.Status;
            existingTask.Priority = (int)(taskDto.Priority != 0 ? taskDto.Priority : existingTask.Priority);

            return _taskRepository.Update(existingTask);
        }

        public IEnumerable<TaskEntity> GetAllTasks()
        {
            return _taskRepository.GetAll();
        }

        public bool DeleteTask(int id)
        {
            var task = _taskRepository.GetById(id);
            if (task == null) return false;
            return _taskRepository.Delete(id); 
        }
    }
}
