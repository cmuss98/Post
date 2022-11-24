using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts;

public class UpdatePost
{
    public class UpdatePostCommand : IRequest<Post>
    {
        public int Id { get; set; }
        public string Content{get;set;}
        public string Title { get; set; }
        public string Image { get; set; }   
    }

    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Post>
    {
        private readonly DataContext _context;

        public UpdatePostCommandHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<Post> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == request.Id);
            if (post == null)
                throw new Exception("Post does not exist");
            post.Title = request.Title;
            post.Image = request.Image;

            var result = await _context.SaveChangesAsync() < 0;
            if (result)
                throw new Exception("An error occured updating user");
            return post;
        }
    }
    

}