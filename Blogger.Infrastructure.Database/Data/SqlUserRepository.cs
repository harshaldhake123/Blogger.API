using Blogger.UseCases.Core.Entities;
using Blogger.UseCases.Core.Interfaces;

namespace Blogger.Infrastructure.Database.Data;

public class SqlUserRepository : IUserRepository
{
    public User CreateUser(User user)
    {
        throw new NotImplementedException();
    }

    public bool EmailAddressAlreadyExists(string emailAddress)
    {
        throw new NotImplementedException();
    }
}