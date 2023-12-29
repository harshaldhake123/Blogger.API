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
            ID = 1,
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
            Password = "user input password",
        };

        User expected = new()
        {
            ID = 1,
            FirstName = "FirstName",
            LastName = "LastName",
            EmailAddress = "first.last@email.com",
            Password = "hashedPassword",
        };
        _userRepository.GetUser(user.EmailAddress).Returns(expected);
        _userAuthenticationService.VerifyPassword(Arg.Any<User>(), expected.Password).Returns(true);

        var loggedIn = await _userService.LoginUser(user);

        Assert.Equal(expected, loggedIn);
        await _userRepository.Received(1).GetUser(user.EmailAddress);
    }

    [Fact]
    public async Task LoginUserShouldReturnFalseWhenUserDoesntExistInDb()
    {
        User user = new()
        {
            EmailAddress = "first.last@email.com",
            Password = "xsfwoq455",
        };

        await Assert.ThrowsAsync<UserNotFoundException>(async () => await _userService.LoginUser(user));

        await _userRepository.Received(1).GetUser(user.EmailAddress);
    }

    [Fact]
    public async Task LoginUserShouldReturnFalseWhenPasswordVerificationFails()
    {
        User user = new()
        {
            EmailAddress = "first.last@email.com",
            Password = "user input password",
        };

        User expected = new()
        {
            ID = 1,
            FirstName = "FirstName",
            LastName = "LastName",
            EmailAddress = "first.last@email.com",
            Password = "hashedPassword",
        };
        _userRepository.GetUser(user.EmailAddress).Returns(expected);
        _userAuthenticationService.VerifyPassword(Arg.Any<User>(), expected.Password).Returns(false);

        await Assert.ThrowsAsync<InvalidPasswordException>(async () => await _userService.LoginUser(user));
        await _userRepository.Received(1).GetUser(user.EmailAddress);
    }
}