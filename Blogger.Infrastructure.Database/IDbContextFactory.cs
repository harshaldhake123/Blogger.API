namespace Blogger.Infrastructure.Database;

public interface IDbContextFactory
{
    BloggerDbContext CreateBloggerDbContext();
}
