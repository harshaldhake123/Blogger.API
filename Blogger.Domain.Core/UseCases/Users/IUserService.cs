using Blogger.Domain.Core.Entities;

namespace Blogger.Domain.Core.UseCases.Users;

public interface IUserService
{
    public Task CreateUser(User user);

    public Task<bool> LoginUser(User user);
}