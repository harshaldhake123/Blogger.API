using Blogger.Domain.Core.Entities;
using Blogger.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Blogger.Domain.Core.UseCases.Users;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserRepository _userRepository;

    public UserAuthenticationService(IPasswordHasher<User> passwordHasher, IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public string HashPassword(User user)
    {
        return _passwordHasher.HashPassword(user, user.Password);
    }

    public async Task<bool> VerifyPassword(User user, string storedHashedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, storedHashedPassword, user.Password);
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
        await _userRepository.UpdateUser(user);
    }
}