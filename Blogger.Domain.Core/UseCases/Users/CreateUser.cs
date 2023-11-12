using Blogger.UseCases.Core.Entities;
using Blogger.UseCases.Core.Exceptions;
using Blogger.UseCases.Core.Interfaces;

namespace Blogger.UseCases.Core.UseCases.Users
{
    public class CreateUser
    {
        private readonly IUserRepository _userRepository;

        public CreateUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Execute(User user)
        {
            if (_userRepository.EmailAddressAlreadyExists(user.EmailAddress))
            {
                throw new DuplicateEmailException();
            }
            return _userRepository.CreateUser(user);
        }
    }
}