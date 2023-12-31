using Blogger.Core.Entities;

namespace Blogger.Application.Abstractions;

public interface IUserService
{
    public Task<User> CreateUser(User user);

    public Task<User> LoginUser(User user);
}