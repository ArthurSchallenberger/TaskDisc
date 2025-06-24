using Api_Restful.Application.Interfaces;
using Api_Restful.Presentation.Dto;
using Api_Restful.Core.Entities;
using AutoMapper;

namespace Api_Restful.Application.Services
{
    public class TaskManagementService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskManagementService(
            ITaskRepository taskRepository,
            IMapper mapper
        )
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<TaskEntity> CreateTask(TaskDto taskDto)
        {
            if (taskDto is null)
            {
                throw new ArgumentNullException(nameof(taskDto), "Task data cannot be null.");
            }

            var task = _mapper.Map<TaskEntity>(taskDto);

            return await _taskRepository.Add(task);
        }

        public async Task<TaskDto> GetTaskById(int id)
        {
            var taskQuery = await _taskRepository.GetById(id);

            if (taskQuery is null)
            {
                throw new KeyNotFoundException($"Task with ID {id} not found.");
            }

            return _mapper.Map<TaskDto>(taskQuery);
        }

        public async Task<TaskDto> UpdateTask(TaskDto taskDto)
        {
            if (taskDto is null)
            {
                throw new ArgumentNullException(nameof(taskDto), "Task data cannot be null.");
            }

            var existingTask = await _taskRepository.GetById(taskDto.Id);

            if (existingTask is null)
            {
                throw new KeyNotFoundException($"Task with ID {taskDto.Id} not found.");
            }

            _mapper.Map(taskDto, existingTask);

            var updatedTask = await _taskRepository.Update(existingTask);

            return _mapper.Map<TaskDto>(updatedTask);
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAll();

            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<bool> DeleteTask(int id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null) return false;

            return await _taskRepository.Delete(id);
        }
    }
}
