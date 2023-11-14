using Blogger.UseCases.Core.Entities;
using Blogger.UseCases.Core.Exceptions;
using Blogger.UseCases.Core.Interfaces;

namespace Blogger.Domain.Core.UseCases.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task CreateUser(User user)
    {
        if (await _userRepository.EmailAddressAlreadyExists(user.EmailAddress))
        {
            throw new DuplicateEmailException();
        }
       await  _userRepository.CreateUser(user);
    }
}