using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogger.Domain.Core.Entities;

[Table("tbl_Users")]
public class User
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [MaxLength(50)]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string EmailAddress { get; set; }

    [Required]
    public string Password { get; set; }
}