using Api_Restful.Application.Interfaces;
using Api_Restful.Presentation.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Api_Restful.Application.Services
{
    public class TaskUserManagementService : ITaskUserService
    {

        private readonly ITaskUserRepository _taskUserRepository;
        public  TaskUserManagementService(ITaskUserRepository taskUserRepository)
        {
            _taskUserRepository = taskUserRepository;
        }

        public async Task<TaskUserDto> GetByTaskId(int taskId)
        {
            var query = _taskUserRepository.GetByTaskId(taskId);
            var returnDto = new TaskUserDto
            {
                ID_Task = query.ID_Task,
                ID_User = query.ID_User,
                Task = new TaskDto
                {
                    ID_PK = query.Task.ID_PK, 
                    Description = query.Task.Description, 
                    Status = query.Task.Status
                },
                User = new UserDto 
                {
                    ID_PK = query.User.ID_PK, 
                    Name = query.User.Name, 
                    Email = query.User.Email
                } 
            };


            return returnDto;
        }

        public async Task<TaskUser> GetByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskUser>Update(TaskUser taskUser)
        {
            throw new NotImplementedException();
        }
    }
}
