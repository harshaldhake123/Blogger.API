using Blogger.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blogger.Domain.Core.UseCases.Users;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserAuthenticationService(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string HashPassword(User user)
    {
        return _passwordHasher.HashPassword(user, user.Password);
    }

    public PasswordVerificationResult VerifyPassword(User user, string storedHashedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(user, storedHashedPassword, user.Password);
    }
}