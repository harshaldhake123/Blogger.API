using Blogger.Application.Abstractions;
using Blogger.Core.Abstractions;
using Blogger.Core.Entities;
using Blogger.Core.Exceptions;

namespace Blogger.Application.Services;

public class UserService(
    IUserRepository userRepository,
    IUserAuthenticationService userAuthenticationService) : IUserService
{
    public async Task<User> CreateUser(User user)
    {
        user.Password = userAuthenticationService.HashPassword(user);
        if (await userRepository.EmailAddressAlreadyExists(user.EmailAddress))
        {
            throw new DuplicateEmailException();
        }
        return await userRepository.CreateUser(user);
    }

    public async Task<User> LoginUser(User user)
    {
        var matchedUser = await userRepository.GetUser(user.EmailAddress) ?? throw new UserNotFoundException();
        return await userAuthenticationService.VerifyPassword(user, matchedUser.Password)
            ? matchedUser : throw new InvalidPasswordException();
    }
}