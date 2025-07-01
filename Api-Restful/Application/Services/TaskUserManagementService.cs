using TaskDisc.Application.Interfaces;
using TaskDisc.Core.Entities;
using TaskDisc.Presentation.Dto;
using AutoMapper;

namespace TaskDisc.Application.Services;

public class TaskUserManagementService : ITaskUserService
{

    private readonly ITaskUserRepository _taskUserRepository;
    private readonly IMapper _mapper;
    public TaskUserManagementService(
        ITaskUserRepository taskUserRepository,
        IMapper mapper
    )
    {
        _taskUserRepository = taskUserRepository;
        _mapper = mapper;
    }

    public async Task<TaskUserEntity> CreateTaskUser(TaskUserDto taskUserDto)
    {
        if (taskUserDto is null)
        {
            throw new ArgumentNullException(nameof(taskUserDto), "TaskUserDto cannot be null.");
        }

        var taskUserEntity = _mapper.Map<TaskUserEntity>(taskUserDto);

        return await _taskUserRepository.Add(taskUserEntity);
    }

    public async Task<bool> DeleteTaskUser(int id)
    {
        var taskUser = await _taskUserRepository.GetById(id);
        if (taskUser == null)
        {
            throw new KeyNotFoundException($"TaskUser with ID {id} not found.");
        }
        return await _taskUserRepository.Delete(id);
    }


    public async Task<TaskUserDto> GetByTaskId(int taskId)
    {
        var taskUserQuery = await _taskUserRepository.GetByTaskId(taskId);

        if (taskUserQuery is null)
        {
            throw new KeyNotFoundException($"TaskUser with Task ID {taskId} not found.");
        }

        return _mapper.Map<TaskUserDto>(taskUserQuery);

    }

    public async Task<TaskUserDto> GetByUserId(int id)
    {
        var taskUserQuery = await _taskUserRepository.GetByUserId(id);

        if (taskUserQuery is null)
        {
            throw new KeyNotFoundException($"TaskUser with ID {id} not found.");
        }

        return _mapper.Map<TaskUserDto>(taskUserQuery);
    }

    public async Task<TaskUserDto> Update(TaskUserDto taskUser)
    {

        if (taskUser is null)
        {
            throw new ArgumentNullException(nameof(taskUser), "TaskUserDto cannot be null.");
        }

        var existingTaskUser = await _taskUserRepository.GetById(taskUser.Id);
        if (existingTaskUser is null)
        {
            throw new KeyNotFoundException($"TaskUser with ID {taskUser.Id} not found.");
        }

        _mapper.Map(taskUser, existingTaskUser);

        var updatedTaskUser = await _taskUserRepository.Update(existingTaskUser);

        return _mapper.Map<TaskUserDto>(updatedTaskUser);
    }


}
