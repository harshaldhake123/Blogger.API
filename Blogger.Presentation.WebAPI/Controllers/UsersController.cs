using Blogger.Presentation.WebAPI.DTOs;
using Blogger.Presentation.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Presentation.WebAPI.Controllers;

public class UsersController(IApplicationUserService applicationUserService) : ApiControllerBase
{
    [HttpPost("signup")]
    public async Task<ActionResult> CreateUser(UserRegistrationRequest request)
    {
        await applicationUserService.RegisterUser(request);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(UserLoginRequest request)
    {
        return Ok(await applicationUserService.LoginUser(request));
    }
}