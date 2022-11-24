using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts;

public class ListPostById
{
    public class ListPostByIdQuery : IRequest<Post>
    {
        public int Id { get; set; }
    }

    public class ListPostByIdQueryHandler : IRequestHandler<ListPostByIdQuery, Post>
    {
        private readonly DataContext _context;

        public ListPostByIdQueryHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Post> Handle(ListPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (post is null)
                throw new Exception("Post doesnt exist!");
            return post;
        }
    }
}