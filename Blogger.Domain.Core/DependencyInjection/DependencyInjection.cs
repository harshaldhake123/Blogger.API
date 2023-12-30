using Blogger.Domain.Core.Entities;
using Blogger.Domain.Core.Interfaces;
using Blogger.Domain.Core.UseCases.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Blogger.Domain.Core.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddScoped<IUserAuthenticationService, UserAuthenticationService>();
        return services;
    }
}