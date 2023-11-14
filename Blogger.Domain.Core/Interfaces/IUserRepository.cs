using Blogger.Domain.Core.Entities;

namespace Blogger.Domain.Core.Interfaces;

public interface IUserRepository
{
    public Task CreateUser(User user);

    public Task<bool> EmailAddressAlreadyExists(string emailAddress);
}