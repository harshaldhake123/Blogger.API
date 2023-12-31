using Blogger.API.Contracts.Request;
using Blogger.API.Controllers;
using Blogger.Application.Abstractions;
using NSubstitute;

namespace Blogger.API.UnitTests;

public class UsersControllerTests
{
    private readonly UsersController _usersController;
    private readonly IUserApplicationService _applicationUserService;

    public UsersControllerTests()
    {
        _applicationUserService = Substitute.For<IUserApplicationService>();
        _usersController = new UsersController(_applicationUserService);
    }

    [Fact]
    public async Task PostCreatesUserAndReturnsOKResult()
    {
        UserRegistrationRequest user = new()
        {
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "abc@xyz.com",
            Password = "abcd",
        };
        await _usersController.CreateUser(user);
        await _applicationUserService.Received(1).RegisterUser(user);
    }

    [Fact]
    public async Task GetLogsInUserAndReturnsOKResult()
    {
        UserLoginRequest user = new()
        {
            EmailAddress = "abc@xyz.com",
            Password = "abcd",
        };
        await _usersController.LoginUser(user);
        await _applicationUserService.Received(1).LoginUser(user);
    }
}