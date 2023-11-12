using Blogger.Infrastructure.Database;
using Blogger.UseCases.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Application.API.Controllers;

public class UsersController : ApiControllerBase
{
    private readonly BloggerDbContext _context;

    public UsersController(BloggerDbContext context)
    {
        _context = context;
    }

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