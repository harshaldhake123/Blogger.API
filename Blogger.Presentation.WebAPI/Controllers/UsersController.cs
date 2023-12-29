using Blogger.Presentation.WebAPI.DTOs;
using Blogger.Presentation.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Presentation.WebAPI.Controllers;

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