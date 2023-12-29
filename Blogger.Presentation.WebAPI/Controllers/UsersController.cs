using Blogger.Presentation.WebAPI.DTOs;
using Blogger.Presentation.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Presentation.WebAPI.Controllers;

public class UsersController : ApiControllerBase
{
    private readonly IApplicationUserService _applicationUserService;

    public UsersController(IApplicationUserService applicationUserService)
    {
        _applicationUserService = applicationUserService;
    }

    [HttpPost("signup")]
    public async Task<ActionResult> CreateUser(UserRegistrationRequest request)
    {
        await _applicationUserService.RegisterUser(request);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(UserLoginRequest request)
    {
        return Ok(await _applicationUserService.LoginUser(request));
    }
}