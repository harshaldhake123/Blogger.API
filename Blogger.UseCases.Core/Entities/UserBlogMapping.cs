namespace Blogger.UseCases.Core.Entities;

public class UserBlogMapping
{
    public long ID { get; set; }
    public long UserID { get; set; }
    public long BlogID { get; set; }
    public virtual User User { get; set; }
    public virtual Blog Blog { get; set; }
}