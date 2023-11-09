using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blogger.Entities
{
    public class BloggerDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BloggerDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                _configuration.GetConnectionString("BloggerDbContext")
                ?? throw new InvalidOperationException("Connection string 'BloggerDbContext' not found."),
                builder => builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null));
        }

        public DbSet<User> User { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<UserBlogMapping> UserBlogMappings { get; set; }
    }
}