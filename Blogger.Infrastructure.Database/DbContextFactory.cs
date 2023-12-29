using Microsoft.Extensions.DependencyInjection;

namespace Blogger.Infrastructure.Database;

public class DbContextFactory(IServiceScopeFactory serviceScopeFactory) : IDbContextFactory
{
    public BloggerDbContext CreateBloggerDbContext()
    {
        using var scope = serviceScopeFactory.CreateScope();
        return scope.ServiceProvider.GetRequiredService<BloggerDbContext>();
    }
}