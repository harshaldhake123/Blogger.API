using Blogger.Domain.Core.Entities;

namespace Blogger.Domain.Core.Interfaces;

public interface IUserRepository
{
    public Task<User> CreateUser(User user);

    public Task<bool> EmailAddressAlreadyExists(string emailAddress);

    Task<User?> GetUser(string emailAddress);

    Task UpdateUser(User user);
}