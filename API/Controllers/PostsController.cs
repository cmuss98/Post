using Application.Dto;
using Application.Posts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
//se nao estiveres autenticado nao podes criar um post
public class PostsController: BaseApiController
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> Create(CreatePost.CreatePostCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<PostDto>>> GetAllPosts()
    {
        return await _mediator.Send(new ListPosts.ListPostsQuery());
    }
   
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPostById(int id)
    {
        return await _mediator.Send(new ListPostById.ListPostByIdQuery { Id = id });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Post>> UpdatePost(UpdatePost.UpdatePostCommand command, int id)
    {
        command.Id = id;
        return await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Post>> DeletePost(DeletePost.DeletePostCommand command, int id)
    {
        command.Id = id;
        return await _mediator.Send(command);
    }
}