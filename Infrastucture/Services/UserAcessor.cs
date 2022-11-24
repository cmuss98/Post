using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Application.Interfaces;

namespace Infrastucture.Services;
public class UserAcessor: IUserAcessor{
    private readonly IHttpContextAccessor _httpContextAcessor;
    
    public UserAcessor(IHttpContextAccessor httpContextAcessor){
        _httpContextAcessor=httpContextAcessor;
    }
    public string GetCurrentUserId(){
        var userId=_httpContextAcessor.HttpContext.User.Claims.First(
            x=> x.Type==ClaimTypes.NameIdentifier
        ).Value;
        return userId;
    }

}