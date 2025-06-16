using Api_Restful.Application.Interfaces;
using Api_Restful.Core.UseCases;

namespace Api_Restful.Application.Services
{
    public class TaskManagementService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskManagementService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task CreateTask(Task task)
        {
            // Lógica de validação e persistência
            return _taskRepository.Add(task);
        }

        public Task GetTaskById(int id)
        {
            return _taskRepository.GetById(id);
        }


        public Task UpdateTask(Task task)
        {
            // Lógica de validação e atualização
            return _taskRepository.Update(task);
        }

        public bool DeleteTask(int id)
        {
            // Lógica de soft delete
            return _taskRepository.Delete(id);
        }
    }
}
