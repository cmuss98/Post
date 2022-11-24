using Application.Interfaces;
using Domain;

namespace Infrastucture.Services;

public class UserList: IUserNamesList
{
    public List<string> GetUserNames(List<User> users)
    {
        return users.Select(x => x.UserName).ToList();
    }
}