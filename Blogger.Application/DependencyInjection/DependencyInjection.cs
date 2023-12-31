using Blogger.Application.Abstractions;
using Blogger.Application.Services;
using Blogger.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Blogger.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddScoped<IUserAuthenticationService, UserAuthenticationService>()
             .AddScoped<IUserApplicationService, UserApplicationService>();
        return services;
    }
}