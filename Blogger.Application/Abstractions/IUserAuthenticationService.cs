using Blogger.Core.Entities;

namespace Blogger.Application.Abstractions;

public interface IUserAuthenticationService
{
    public string HashPassword(User user);

    public Task<bool> VerifyPassword(User user, string storedHashedPassword);
}