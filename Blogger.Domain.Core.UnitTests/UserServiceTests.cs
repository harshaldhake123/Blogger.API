using Blogger.Domain.Core.UseCases.Users;
using Blogger.UseCases.Core.Entities;
using Blogger.UseCases.Core.Exceptions;
using Blogger.UseCases.Core.Interfaces;
using NSubstitute;

namespace Blogger.Domain.Core.UnitTests;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly IUserRepository _userRepository;

    public UserServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _userService = new UserService(_userRepository);
    }

    [Fact]
    public async Task Should_create_User()
    {
        User expected = new()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            EmailAddress = "first.last@email.com",
            Password = "xsfwoq455",
        };

        await _userService.CreateUser(expected);

        await _userRepository.Received().CreateUser(expected);
    }

    [Fact]
    public async Task When_User_EmailAddress_already_exists_Then_should_throw_InvalidOperationException()
    {
        User expected = new()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            EmailAddress = "first.last@email.com",
            Password = "xsfwoq455",
        };
        _userRepository.EmailAddressAlreadyExists(expected.EmailAddress).Returns(true);

        await Assert.ThrowsAsync<DuplicateEmailException>(() => _userService.CreateUser(expected));
    }
}