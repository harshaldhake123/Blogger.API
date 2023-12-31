using Blogger.Core.Abstractions;
using Blogger.Core.Entities;
using Blogger.Database.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Database.Repositories;

public class SqlUserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    public async Task<User> CreateUser(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> EmailAddressAlreadyExists(string emailAddress)
    {
        return await dbContext.Users.AnyAsync(u => u.EmailAddress == emailAddress);
    }

    public async Task<User?> GetUser(string emailAddress)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.EmailAddress == emailAddress);
    }

    public async Task UpdateUser(User user)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
    }
}