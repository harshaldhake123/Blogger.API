using Blogger.Presentation.WebAPI.DTOs;

namespace Blogger.Presentation.WebAPI.Services;

public interface IUserApplicationService
{
    public Task<UserProfileResponse> RegisterUser(UserRegistrationRequest request);

    public Task<UserProfileResponse> LoginUser(UserLoginRequest request);
}