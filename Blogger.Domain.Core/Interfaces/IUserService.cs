using Blogger.Domain.Core.Entities;

namespace Blogger.Domain.Core.Interfaces;

public interface IUserService
{
    public Task<User> CreateUser(User user);

    public Task<User> LoginUser(User user);
}