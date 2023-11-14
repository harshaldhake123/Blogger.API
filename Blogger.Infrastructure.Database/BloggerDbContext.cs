using Blogger.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Infrastructure.Database;

public class BloggerDbContext : DbContext
{
    public BloggerDbContext(DbContextOptions<BloggerDbContext> options) : base(options)
    { }

    public DbSet<User> User { get; set; }
}