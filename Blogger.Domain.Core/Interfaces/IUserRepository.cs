using Blogger.UseCases.Core.Entities;

namespace Blogger.UseCases.Core.Interfaces;

public interface IUserRepository
{
    public void CreateUser(User user);

    public bool EmailAddressAlreadyExists(string emailAddress);
}