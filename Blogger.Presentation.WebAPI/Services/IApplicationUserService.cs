using Blogger.Domain.Core.Entities;
using Blogger.Presentation.WebAPI.DTOs;

namespace Blogger.Presentation.WebAPI.Services;

public interface IApplicationUserService
{
    public Task RegisterUser(UserRegistrationRequest request);

    public Task<User> LoginUser(UserLoginRequest request);
}