using Blogger.Domain.Core.Entities;
using Blogger.Domain.Core.UseCases.Users;
using Blogger.Presentation.WebAPI.Controllers;

namespace Blogger.Presentation.WebAPI.UnitTests;

public class UsersControllerTests
{
    private readonly UsersController _usersController;
    private readonly IUserService _userService;

    public UsersControllerTests()
    {
        _userService = Substitute.For<IUserService>();
        _usersController = new UsersController(_userService);
    }

    [Fact]
    public async Task PostCreatesUserAndReturnsOKResult()
    {
        User user = new()
        {
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "abc@xyz.com",
            Password = "abcd",
        };
        await _usersController.CreateUser(user);
        await _userService.Received(1).CreateUser(user);
    }

    [Fact]
    public async Task GetLogsInUserAndReturnsOKResult()
    {
        User user = new()
        {
            EmailAddress = "abc@xyz.com",
            Password = "abcd",
        };
        await _usersController.LoginUser(user);
        await _userService.Received(1).LoginUser(user);
    }
}