using Blogger.Domain.Core.Entities;
using Blogger.Domain.Core.UseCases.Users;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Presentation.WebAPI.Controllers;

public class UsersController : ApiControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(User user)
    {
        await _userService.CreateUser(user);
        return Ok();
    }
}