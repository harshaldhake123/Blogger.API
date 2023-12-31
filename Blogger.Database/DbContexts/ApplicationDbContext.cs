using Blogger.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Database.DbContexts;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}