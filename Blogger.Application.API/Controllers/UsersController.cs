using Blogger.Infrastructure.Database;
using Blogger.UseCases.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Application.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly BloggerDbContext _context;

    public UsersController(BloggerDbContext context)
    {
        _context = context;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUser()
    {
        if (_context.User == null)
        {
            return NotFound();
        }
        return await _context.User.ToListAsync();
    }
}