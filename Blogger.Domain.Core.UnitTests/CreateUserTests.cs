using Blogger.UseCases.Core.Entities;
using Blogger.UseCases.Core.Exceptions;
using Blogger.UseCases.Core.Interfaces;
using Blogger.UseCases.Core.UseCases.Users;
using NSubstitute;

namespace Blogger.Domain.Core.UnitTests
{
    public class CreateUserTests
    {
        private readonly CreateUser _createUser;
        private readonly IUserRepository _userRepository;

        public CreateUserTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _createUser = new CreateUser(_userRepository);
        }

        [Fact]
        public void Should_create_User()
        {
            User expected = new()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                EmailAddress = "first.last@email.com",
                Password = "xsfwoq455",
            };

            _createUser.Execute(expected);

            _userRepository.Received().CreateUser(expected);
        }

        [Fact]
        public void When_User_EmailAddress_already_exists_Then_should_throw_InvalidOperationException()
        {
            User expected = new()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                EmailAddress = "first.last@email.com",
                Password = "xsfwoq455",
            };
            _userRepository.EmailAddressAlreadyExists(expected.EmailAddress).Returns(true);

            Assert.Throws<DuplicateEmailException>(() => _createUser.Execute(expected));
        }
    }
}