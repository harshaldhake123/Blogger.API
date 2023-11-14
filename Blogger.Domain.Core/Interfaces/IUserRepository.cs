using Blogger.UseCases.Core.Entities;

namespace Blogger.UseCases.Core.Interfaces;

public interface IUserRepository
{
    public Task CreateUser(User user);

    public Task<bool> EmailAddressAlreadyExists(string emailAddress);
}