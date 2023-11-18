using Blogger.Domain.Core.Entities;
using Blogger.Domain.Core.Exceptions;
using Blogger.Domain.Core.Interfaces;
using Blogger.Domain.Core.UseCases.Users;
using NSubstitute;

namespace Blogger.Domain.Core.UnitTests;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IUserAuthenticationService _userAuthenticationService;

    public UserServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _userAuthenticationService = Substitute.For<IUserAuthenticationService>();
        _userService = new UserService(_userRepository, _userAuthenticationService);
    }

    [Fact]
    public async Task ShouldCreateUser()
    {
        User expected = new()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            EmailAddress = "first.last@email.com",
            Password = "abcd",
        };

        await _userService.CreateUser(expected);

        await _userRepository.Received().CreateUser(expected);
    }

    [Fact]
    public async Task WhenUserEmailAddressAlreadyExistsThenShouldThrowInvalidOperationException()
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

    [Fact]
    public async Task ShouldLoginUser()
    {
        User user = new()
        {
            EmailAddress = "first.last@email.com",
            Password = "xsfwoq455",
        };
        await _userService.LoginUser(user);

        await _userRepository.Received(1).GetUser(user.EmailAddress);
    }
}