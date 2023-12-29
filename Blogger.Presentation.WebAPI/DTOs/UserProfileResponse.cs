using System.ComponentModel.DataAnnotations;

namespace Blogger.Presentation.WebAPI.DTOs
{
    public class UserProfileResponse
    {
        public long ID { get; set; }

        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; }
    }
}