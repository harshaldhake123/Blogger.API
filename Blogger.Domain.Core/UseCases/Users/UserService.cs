using Blogger.Domain.Core.Entities;
using Blogger.Domain.Core.Exceptions;
using Blogger.Domain.Core.Interfaces;

namespace Blogger.Domain.Core.UseCases.Users;

public class UserService(
    IUserRepository userRepository,
    IUserAuthenticationService userAuthenticationService) : IUserService
{
    public async Task CreateUser(User user)
    {
        user.Password = userAuthenticationService.HashPassword(user);
        if (await userRepository.EmailAddressAlreadyExists(user.EmailAddress))
        {
            throw new DuplicateEmailException();
        }
        await userRepository.CreateUser(user);
    }

    public async Task<User> LoginUser(User user)
    {
        var matchedUser = await userRepository.GetUser(user.EmailAddress) ?? throw new UserNotFoundException();
        return await userAuthenticationService.VerifyPassword(user, matchedUser.Password)
            ? matchedUser : throw new InvalidPasswordException();
    }
}