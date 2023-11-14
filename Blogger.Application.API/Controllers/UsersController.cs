using Blogger.Domain.Core.UseCases.Users;
using Blogger.UseCases.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Application.API.Controllers;

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