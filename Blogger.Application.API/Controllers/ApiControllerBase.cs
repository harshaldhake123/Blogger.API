using Microsoft.AspNetCore.Mvc;

namespace Blogger.Application.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
}