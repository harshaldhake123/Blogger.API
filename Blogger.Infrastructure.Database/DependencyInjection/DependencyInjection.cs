﻿using Blogger.Domain.Core.Interfaces;
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
            //.AddDbContext(configuration)
            .AddSingleton<IDbContextFactory, DbContextFactory>()
            .AddSingleton<IUserRepository, SqlUserRepository>();
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<BloggerDbContext>(options =>
                            options.UseSqlServer(configuration.GetConnectionString("BloggerDbContext") ?? throw new InvalidOperationException("'BloggerDbContext' ConnectionString not found."),
                            builder => builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null))
                        );
        return services;
    }
}