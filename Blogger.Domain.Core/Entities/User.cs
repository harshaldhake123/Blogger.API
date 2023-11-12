using System.ComponentModel.DataAnnotations;

namespace Blogger.UseCases.Core.Entities;

public class User
{
    public long ID { get; set; }

    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string LastName { get; set; }

    [EmailAddress]
    public string EmailAddress { get; set; }

    public string Password { get; set; }
}