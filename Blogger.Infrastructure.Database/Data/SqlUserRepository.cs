using Blogger.Domain.Core.Entities;
using Blogger.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Infrastructure.Database.Data;

public class SqlUserRepository(IDbContextFactory dbContextFactory) : IUserRepository
{
    public async Task<User> CreateUser(User user)
    {
        using var bloggerDbContext = dbContextFactory.CreateBloggerDbContext();
        await bloggerDbContext.Users.AddAsync(user);
        await bloggerDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> EmailAddressAlreadyExists(string emailAddress)
    {
        using var bloggerDbContext = dbContextFactory.CreateBloggerDbContext();
        return await bloggerDbContext.Users.AnyAsync(u => u.EmailAddress == emailAddress);
    }

    public async Task<User?> GetUser(string emailAddress)
    {
        using var bloggerDbContext = dbContextFactory.CreateBloggerDbContext();
        return await bloggerDbContext.Users.FirstOrDefaultAsync(u => u.EmailAddress == emailAddress);
    }

    public async Task UpdateUser(User user)
    {
        using var bloggerDbContext = dbContextFactory.CreateBloggerDbContext();
        bloggerDbContext.Users.Update(user);
        await bloggerDbContext.SaveChangesAsync();
    }
}