using Blogger.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Infrastructure.Database;

public class BloggerDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}