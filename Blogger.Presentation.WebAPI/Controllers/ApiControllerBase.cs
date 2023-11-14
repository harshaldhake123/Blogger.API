using Microsoft.AspNetCore.Mvc;

namespace Blogger.Presentation.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
}