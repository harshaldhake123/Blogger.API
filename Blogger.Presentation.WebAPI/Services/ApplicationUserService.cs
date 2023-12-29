using Blogger.Domain.Core.Entities;
using Blogger.Domain.Core.Interfaces;
using Blogger.Presentation.WebAPI.DTOs;

namespace Blogger.Presentation.WebAPI.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly IUserService _userService;

    public ApplicationUserService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<User> LoginUser(UserLoginRequest request)
    {
        var user = new User
        {
            EmailAddress = request.EmailAddress,
            Password = request.Password
        };
        return await _userService.LoginUser(user);
    }

    public async Task RegisterUser(UserRegistrationRequest request)
    {
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailAddress = request.EmailAddress,
            Password = request.Password
        };
        await _userService.CreateUser(user);
    }
}