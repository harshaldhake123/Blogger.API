using System.ComponentModel.DataAnnotations;

namespace Blogger.Presentation.WebAPI.DTOs
{
    public class UserLoginRequest
    {
        [EmailAddress]
        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }
}