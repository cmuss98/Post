using System.Net;
using Application.Dto;
using Application.Errors;
using Application.Interfaces;
using Application.Users;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts;

public class CreatePost
{
    public class CreatePostCommand : IRequest<PostDto>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; } 
        public string Content { get; set; }  

    }

    public class CreatePostValidator: AbstractValidator<CreatePostCommand>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Image).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
        }
    }

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IPostRepository _postRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAcessor _userAcessor;

        public CreatePostCommandHandler( DataContext context,UserManager<User> userManager, 
            IPostRepository postRepository, IMapper mapper, IUserAcessor UserAcessor)
        {
            _userManager = userManager;
            _postRepository = postRepository;
            _context = context;
            _userAcessor = UserAcessor;
            _mapper = mapper;


        }
        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var postFound = await _context.Posts.FirstOrDefaultAsync(p=>p.Title == request.Title, cancellationToken);
            if (postFound != null)
            {
                throw new Exception("A post with the same title already exists");
            }

            var user = await _userManager.FindByIdAsync(_userAcessor.GetCurrentUserId());
            var post = new Post()
            {
                Title = request.Title,
                Image = request.Image,
                CreationDate = DateTimeOffset.UtcNow,
                Content=request.Content,
                User=user
            };
            Console.WriteLine(post);
            
            _postRepository.Add(post);
            var result = await _postRepository.Complete() < 0;
            if (result)
            {
                throw new RestException(HttpStatusCode.InternalServerError, "An Error occurred, post not saved");
            }
            return _mapper.Map<Post,PostDto>(post);
        }
    }

}