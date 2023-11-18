using Blogger.Domain.Core.Entities;
using Blogger.Domain.Core.Exceptions;
using Blogger.Domain.Core.Interfaces;

namespace Blogger.Domain.Core.UseCases.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserAuthenticationService _userAuthenticationService;

    public UserService(IUserRepository userRepository, IUserAuthenticationService userAuthenticationService)
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
}