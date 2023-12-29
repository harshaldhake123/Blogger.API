using Blogger.Domain.Core.Entities;
using Blogger.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Infrastructure.Database.Data;

public class SqlUserRepository(BloggerDbContext bloggerDbContext) : IUserRepository
{
    public async Task<User> CreateUser(User user)
    {
        await bloggerDbContext.User.AddAsync(user);
        await bloggerDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> EmailAddressAlreadyExists(string emailAddress)
    {
        return await bloggerDbContext.User.AnyAsync(u => u.EmailAddress == emailAddress);
    }

    public async Task<User?> GetUser(string emailAddress)
    {
        return await bloggerDbContext.User.FirstOrDefaultAsync(u => u.EmailAddress == emailAddress);
    }

    public async Task UpdateUser(User user)
    {
        bloggerDbContext.User.Update(user);
        await bloggerDbContext.SaveChangesAsync();
    }
}