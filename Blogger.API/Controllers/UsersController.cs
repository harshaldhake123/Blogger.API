using Blogger.API.Contracts.Request;
using Blogger.API.Contracts.Response;
using Blogger.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.API.Controllers;

public class UsersController(IUserApplicationService applicationUserService) : ApiControllerBase
{
    [HttpPost("signup")]
    public async Task<ActionResult<UserProfileResponse>> CreateUser(UserRegistrationRequest request)
    {
        return await applicationUserService.RegisterUser(request);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserProfileResponse>> LoginUser(UserLoginRequest request)
    {
        return await applicationUserService.LoginUser(request);
    }
}