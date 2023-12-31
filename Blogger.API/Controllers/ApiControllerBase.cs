using Microsoft.AspNetCore.Mvc;

namespace Blogger.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
}