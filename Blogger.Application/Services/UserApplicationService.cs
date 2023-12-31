using Blogger.API.Contracts.Request;
using Blogger.API.Contracts.Response;
using Blogger.Application.Abstractions;
using Blogger.Core.Entities;

namespace Blogger.Application.Services;

public class UserApplicationService(IUserService userService) : IUserApplicationService
{
    public async Task<UserProfileResponse> LoginUser(UserLoginRequest request)
    {
        var user = new User
        {
            EmailAddress = request.EmailAddress,
            Password = request.Password
        };
        var loggedInUser = await userService.LoginUser(user);
        return new UserProfileResponse
        {
            FirstName = loggedInUser.FirstName,
            LastName = loggedInUser.LastName,
            EmailAddress = loggedInUser.EmailAddress,
            ID = loggedInUser.Id,
        };
    }

    public async Task<UserProfileResponse> RegisterUser(UserRegistrationRequest request)
    {
        var userToCreate = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailAddress = request.EmailAddress,
            Password = request.Password
        };
        var createdUser = await userService.CreateUser(userToCreate);
        return new UserProfileResponse
        {
            FirstName = createdUser.FirstName,
            LastName = createdUser.LastName,
            EmailAddress = createdUser.EmailAddress,
            ID = createdUser.Id,
        };
    }
}