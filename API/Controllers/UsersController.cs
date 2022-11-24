using Application.Dto;
using Application.Posts;
using Application.Users;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController: BaseApiController
{
    private readonly IMediator _meditor;
    public UsersController(IMediator mediatR)
    {
        _meditor = mediatR;
    }
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(Application.Users.CreateUser.CreateUserCommand command)
    {
        return await _meditor.Send(command);
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> ListUsers()
    {
        return await _meditor.Send(new ListUsers.ListUsersQuery());
    }
/*
    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(UpdateUser.UpdateUserCommand command, int id)
    {
        command.Id = id;
        return await _meditor.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<UserDto>> DeleteUser(DeleteUser.DeleteUserCommand command, int id)
    {
        command.Id = id;
        return await _meditor.Send(command);
    }*/
    
    [HttpGet("usernames")]
    public async Task<List<string>> GetUserNames()
    {
        return await _meditor.Send(new UsernameList.UsernameListQuery());
    }
    
}