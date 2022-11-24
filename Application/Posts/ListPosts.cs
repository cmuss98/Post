using Application.Dto;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts;

public class ListPosts
{
    public class ListPostsQuery : IRequest<List<PostDto>>
    {
        
    }

    public class ListPostsQueryHandler : IRequestHandler<ListPostsQuery, List<PostDto>>
    {
        private readonly DataContext _context;

        public ListPostsQueryHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<List<PostDto>> Handle(ListPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _context.Posts.Include(x=> x.User).ToListAsync(cancellationToken);
            var postsDto = posts.Select(post => new PostDto()
            {
                Id = post.Id, 
                CreationDate = post.CreationDate,
                Image = post.Image,
                Title = post.Title,
                Content=post.Content,
                UserName = post.User.UserName
                
            }).ToList();
            return postsDto;
        }
    }
}