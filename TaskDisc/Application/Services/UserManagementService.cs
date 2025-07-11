﻿using TaskDisc.Application.Interfaces;
using TaskDisc.Core.Entities;
using TaskDisc.Presentation.Dto;
using AutoMapper;

namespace TaskDisc.Application.Services;

public class UserManagementService : IUserService
{

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserManagementService(
        IUserRepository userRepository,
        IMapper mapper
    )
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserEntity> CreateUser(UserDto userDto)
    {
        if (userDto is null)
        {
            throw new ArgumentNullException(nameof(userDto), "User data cannot be null.");
        }

        userDto.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        var user = _mapper.Map<UserEntity>(userDto);

        return await _userRepository.Add(user);
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = _userRepository.GetById(id);
        if (user == null) return false;
        return await _userRepository.Delete(id);
    }

    public async Task<UserDto> GetUserById(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }
        return _mapper.Map<UserDto>(user);



    }

    public async Task<IEnumerable<UserDto>> GetAllUsersByJobTittleId(int idJobTittle)
    {
        var users = await _userRepository.GetByJobTittleId(idJobTittle);
        if (users is null)
        {
            throw new InvalidOperationException("No users found.");
        }
        return _mapper.Map<IEnumerable<UserDto>>(users);

    }

    public async Task<UserDto> UpdateUser(UserDto userDto)
    {
        if( userDto is null)
        {
            throw new ArgumentNullException(nameof(userDto), "User data cannot be null.");
        }   

        var user = await _userRepository.GetById(userDto.Id);

        if(user is null)
        {
            throw new InvalidOperationException("User not found.");
        }

        _mapper.Map(userDto, user);

        var updateUser = await _userRepository.Update(user);

        return _mapper.Map<UserDto>(updateUser);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        var users = await _userRepository.GetAll();

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<IEnumerable<UserDto>> GetAllUserIdAndName()
    {
        var users = await _userRepository.GetAllUserIdAndName();

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<bool> ValidateUserCredentials(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);
        if (user == null) return false;
        
        return BCrypt.Net.BCrypt.Verify(password, user.Password);
    }
}
