using Blogger.Application.Services;
using Blogger.Core.Abstractions;
using Blogger.Core.Entities;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace Blogger.Application.UnitTests;

public class UserAuthenticationServiceTests
{
    private readonly UserAuthenticationService _userAuthenticationService;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserRepository _userRepository;

    public UserAuthenticationServiceTests()
    {
        _passwordHasher = Substitute.For<IPasswordHasher<User>>();
        _userRepository = Substitute.For<IUserRepository>();
        _userAuthenticationService = new UserAuthenticationService(_passwordHasher, _userRepository);
    }

    [Fact]
    public void HashPasswordReturnsHashedPassword()
    {
        User user = new()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            EmailAddress = "first.last@email.com",
            Password = "plain password",
        };
        _passwordHasher.HashPassword(user, user.Password).Returns("hashed password");
        var actual = _userAuthenticationService.HashPassword(user);
        Assert.True(actual.Length > 0);
    }

    [Theory]
    [InlineData(PasswordVerificationResult.Success, true)]
    [InlineData(PasswordVerificationResult.Failed, false)]
    public async Task VerifyPasswordReturnsTrueWhenSuccessful(PasswordVerificationResult verificationResult, bool successful)

    {
        User user = new()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            EmailAddress = "first.last@email.com",
            Password = "plain password",
        };
        _passwordHasher.VerifyHashedPassword(user, "storedHashedPassword", user.Password).Returns(verificationResult);

        var success = await _userAuthenticationService.VerifyPassword(user, "storedHashedPassword");
        Assert.Equal(successful, success);
    }

    [Fact]
    public async Task VerifyPasswordReturnsTrueAndUpdatesUserPasswordWhenPasswordRequiresRehashing()
    {
        User user = new()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            EmailAddress = "first.last@email.com",
            Password = "plain password",
        };
        _passwordHasher.VerifyHashedPassword(user, "storedHashedPassword", user.Password).Returns(PasswordVerificationResult.SuccessRehashNeeded);

        var success = await _userAuthenticationService.VerifyPassword(user, "storedHashedPassword");
        Assert.True(success);
        await _userRepository.Received(1).UpdateUser(user);
    }
}