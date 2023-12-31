using Blogger.Core.Entities;

namespace Blogger.Core.Abstractions;

public interface IUserRepository
{
    public Task<User> CreateUser(User user);

    public Task<bool> EmailAddressAlreadyExists(string emailAddress);

    Task<User?> GetUser(string emailAddress);

    Task UpdateUser(User user);
}