using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blogger.Infrastructure.Database.IntegrationTests;

public class DatabaseIntegrationTestBase : IDisposable
{
    protected readonly ApplicationDbContext _dbContext;

    public DatabaseIntegrationTestBase()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(configuration.GetConnectionString("IntegrationTestDb"))
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _dbContext.Database.Migrate();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}