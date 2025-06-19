using Api_Restful.Application.Interfaces;
using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;

namespace Api_Restful.Application.Services
{
    public class TaskUserManagementService : ITaskUserService
    {

        private readonly ITaskUserRepository _taskUserRepository;
        public  TaskUserManagementService(ITaskUserRepository taskUserRepository)
        {
            _taskUserRepository = taskUserRepository;
        }

        public async Task<TaskUserEntity> CreateTaskUser(TaskUserDto taskUserDto)
        {
            var taskUser = new TaskUserEntity
            {
                Id_Task = taskUserDto.ID_Task,
                Id_User = taskUserDto.ID_User
            };
            var createdTaskUser = _taskUserRepository.Add(taskUser);
            return createdTaskUser;
        }

        public bool DeleteTaskUser(int id)
        {
            return _taskUserRepository.Delete(id);
        }


        public async Task<TaskUserDto> GetByTaskId(int taskId)
        {
            var query = _taskUserRepository.GetByTaskId(taskId);
            var returnDto = new TaskUserDto
            {
                ID_Task = query.Id_Task,
                ID_User = query.Id_User,
                Task = new TaskDto
                {
                    ID_PK = query.Task.Id, 
                    Description = query.Task.Description, 
                    Status = query.Task.Status
                },
                User = new UserDto 
                {
                    ID_PK = query.User.Id, 
                    Name = query.User.Name, 
                    Email = query.User.Email
                } 
            };


            return returnDto;
        }

        public async Task<TaskUserEntity> Update(TaskUserDto taskUser)
        {
            var updatedTaskUser = new TaskUserEntity
            {
                Id_Task = taskUser.ID_Task,
                Id_User = taskUser.ID_User
            };

            return _taskUserRepository.Update(updatedTaskUser);
        }

        Task<TaskUserEntity> ITaskUserService.GetByUserId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
