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
        services.AddSingleton<IUserService, UserService>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IUserAuthenticationService, UserAuthenticationService>();
        return services;
    }
}