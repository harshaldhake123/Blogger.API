using Blogger.Domain.Core.Entities;
using Blogger.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Infrastructure.Database.Data;

public class SqlUserRepository : IUserRepository
{
    private readonly BloggerDbContext _bloggerDbContext;

    public SqlUserRepository(BloggerDbContext bloggerDbContext)
    {
        _bloggerDbContext = bloggerDbContext;
    }

    public async Task CreateUser(User user)
    {
        await _bloggerDbContext.User.AddAsync(user);
        await _bloggerDbContext.SaveChangesAsync();
    }

    public async Task<bool> EmailAddressAlreadyExists(string emailAddress)
    {
        return await _bloggerDbContext.User.AnyAsync(u => u.EmailAddress == emailAddress);
    }

    public async Task<User?> GetUser(string emailAddress)
    {
        return await _bloggerDbContext.User.FirstOrDefaultAsync(u => u.EmailAddress == emailAddress);
    }

    public async Task UpdateUser(User user)
    {
        _bloggerDbContext.User.Update(user);
        await _bloggerDbContext.SaveChangesAsync();
    }
}