using Blogger.API.Contracts.Request;
using Blogger.API.Contracts.Response;

namespace Blogger.Application.Abstractions;

public interface IUserApplicationService
{
    public Task<UserProfileResponse> RegisterUser(UserRegistrationRequest request);

    public Task<UserProfileResponse> LoginUser(UserLoginRequest request);
}