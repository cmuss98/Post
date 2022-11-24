using Application.Accounts;
using Application.Dto;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController: BaseApiController
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<LoginResponse>> Login(Login.LoginCommand command)
    {
        return await _mediator.Send(command);
    }


}