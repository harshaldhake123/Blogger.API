using Blogger.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blogger.Domain.Core.UseCases.Users;

public interface IUserAuthenticationService
{
    public string HashPassword(User user);

    public PasswordVerificationResult VerifyPassword(User user, string storedHashedPassword);
}