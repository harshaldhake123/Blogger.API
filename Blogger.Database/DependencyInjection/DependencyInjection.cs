using Blogger.Core.Abstractions;
using Blogger.Database.DbContexts;
using Blogger.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogger.Database.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApplicationDbContext(configuration)
            .AddScoped<IUserRepository, SqlUserRepository>();
        return services;
    }

    private static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(nameof(ApplicationDbContext))
           ?? throw new InvalidOperationException(nameof(ApplicationDbContext) + " ConnectionString not found.");
        services
           .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        return services;
    }
}