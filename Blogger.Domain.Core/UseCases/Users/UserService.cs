using Blogger.Domain.Core.Entities;
using Blogger.Domain.Core.Exceptions;
using Blogger.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Blogger.Domain.Core.UseCases.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserAuthenticationService _userAuthenticationService;

    public UserService(
        IUserRepository userRepository,
        IUserAuthenticationService userAuthenticationService)
    {
        _userRepository = userRepository;
        _userAuthenticationService = userAuthenticationService;
    }

    public async Task CreateUser(User user)
    {
        user.Password = _userAuthenticationService.HashPassword(user);
        if (await _userRepository.EmailAddressAlreadyExists(user.EmailAddress))
        {
            throw new DuplicateEmailException();
        }
        await _userRepository.CreateUser(user);
    }

    public async Task<bool> LoginUser(User user)
    {
        var userFromDb = await _userRepository.GetUser(user.EmailAddress);
        if (userFromDb == null)
        {
            return false;
        }
        return await VerifyUserPassword(user, userFromDb.Password);
    }

    private async Task<bool> VerifyUserPassword(User user, string storedPassword)
    {
        var result = _userAuthenticationService.VerifyPassword(user, storedPassword);
        switch (result)
        {
            case PasswordVerificationResult.Success:
                return true;

            case PasswordVerificationResult.SuccessRehashNeeded:
                await UpdateUserHashedPassword(user);
                return true;

            default:
                return false;
        }
    }

    private async Task UpdateUserHashedPassword(User user)
    {
        user.Password = _userAuthenticationService.HashPassword(user);
        await _userRepository.UpdateUser(user);
    }
}