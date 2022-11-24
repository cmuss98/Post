using System.Net.Sockets;
using Application.Posts;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IPostRepository
{
    void Add(Post post);
    
    Task<int> Complete();

}