using Blogger.UseCases.Core.Entities;

namespace Blogger.Domain.Core.UseCases.Users;

public interface IUserService
{
    public Task CreateUser(User user);
}