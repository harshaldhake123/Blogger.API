using Blogger.Domain.Core.Entities;
using Blogger.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Infrastructure.Database.Data;

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