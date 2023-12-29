using Blogger.Domain.Core.Entities;
using Blogger.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Blogger.Domain.Core.UseCases.Users;

public class UserAuthenticationService(IPasswordHasher<User> passwordHasher, IUserRepository userRepository) : IUserAuthenticationService
{
    public string HashPassword(User user)
    {
        return passwordHasher.HashPassword(user, user.Password);
    }

    public async Task<bool> VerifyPassword(User user, string storedHashedPassword)
    {
        var result = passwordHasher.VerifyHashedPassword(user, storedHashedPassword, user.Password);
        switch (result)
        {
            case PasswordVerificationResult.Success:
                return true;

            case PasswordVerificationResult.SuccessRehashNeeded:
                await UpdateUserWithRehashedPassword(user);
                return true;

            default:
                return false;
        }
    }

    private async Task UpdateUserWithRehashedPassword(User user)
    {
        user.Password = HashPassword(user);
        await userRepository.UpdateUser(user);
    }
}