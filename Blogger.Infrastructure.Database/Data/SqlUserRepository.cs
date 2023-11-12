using Blogger.UseCases.Core.Entities;
using Blogger.UseCases.Core.Interfaces;

namespace Blogger.Infrastructure.Database.Data;

public class SqlUserRepository : IUserRepository
{
    private readonly BloggerDbContext _bloggerDbContext;

    public SqlUserRepository(BloggerDbContext bloggerDbContext)
    {
        _bloggerDbContext = bloggerDbContext;
    }

    public void CreateUser(User user)
    {
        _bloggerDbContext.User.Add(user);
        _bloggerDbContext.SaveChanges();
    }

    public bool EmailAddressAlreadyExists(string emailAddress)
    {
        return _bloggerDbContext.User.Any(u => u.EmailAddress == emailAddress);
    }
}