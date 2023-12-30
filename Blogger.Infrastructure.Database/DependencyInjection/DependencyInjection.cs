using Blogger.Domain.Core.Interfaces;
using Blogger.Infrastructure.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogger.Infrastructure.Database.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
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