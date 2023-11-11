namespace Blogger.UseCases.Core.Entities;

public class User
{
    public long ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public virtual ICollection<UserBlogMapping> UserBlogMappings { get; set; }
}