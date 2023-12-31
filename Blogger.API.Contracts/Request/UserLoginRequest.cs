using System.ComponentModel.DataAnnotations;

namespace Blogger.API.Contracts.Request;

public class UserLoginRequest
{
    [EmailAddress]
    public string EmailAddress { get; set; }

    public string Password { get; set; }
}