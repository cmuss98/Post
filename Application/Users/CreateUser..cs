using System.Net;
using Application.Dto;
using Application.Errors;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Users;
public class CreateUser
{
    public class CreateUserCommand : IRequest<UserDto>
    {
       
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
    
    
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public CreateUserHandler(DataContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

       

            public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            
            var user = new User()
            {
                FullName = request.FullName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email
                
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return _mapper.Map<UserDto>(user);
            }

            throw new RestException(HttpStatusCode.Conflict, result.Errors);

        }
    }
}
