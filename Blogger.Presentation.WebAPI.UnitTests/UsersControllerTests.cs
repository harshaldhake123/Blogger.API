using Blogger.Presentation.WebAPI.Controllers;
using Blogger.Presentation.WebAPI.DTOs;
using Blogger.Presentation.WebAPI.Services;

namespace Blogger.Presentation.WebAPI.UnitTests;

public class UsersControllerTests
{
    private readonly UsersController _usersController;
    private readonly IApplicationUserService _applicationUserService;

    public UsersControllerTests()
    {
        _applicationUserService = Substitute.For<IApplicationUserService>();
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