using Domain;

namespace Application.Interfaces;

public interface IUserNamesList
{
    public List<string> GetUserNames(List<User> users);

}