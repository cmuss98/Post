using Application.Interfaces;
using Infrastucture.Services;

namespace API.Controllers.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserNamesList, UserList>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUserAcessor, UserAcessor>();
        return services;
    }
}