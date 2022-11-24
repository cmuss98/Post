using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts;

public class DeletePost
{
    public class DeletePostCommand : IRequest<Post>
    {
        public int Id { get; set; }
    }

    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Post>
    {
        private readonly DataContext _context;

        public DeletePostCommandHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<Post> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == request.Id);
            if (post == null)
                throw new Exception("Post does not exist");
            _context.Posts.Remove(post);
            var result = await _context.SaveChangesAsync() < 0;
            if (result)
                throw new Exception("An error occured updating user");
            return post;
        }
    }
}