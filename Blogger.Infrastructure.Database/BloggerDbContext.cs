using Blogger.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Infrastructure.Database;

public class BloggerDbContext(DbContextOptions<BloggerDbContext> options) : DbContext(options)
{
    public DbSet<User> User { get; set; }
}