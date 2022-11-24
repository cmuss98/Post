using Application.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence;

namespace Infrastucture.Services;

public class PostRepository: IPostRepository
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public PostRepository(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public void Add(Post post)
    {
        _context.Set<Post>().Add(post);
    }

    public  async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

}
