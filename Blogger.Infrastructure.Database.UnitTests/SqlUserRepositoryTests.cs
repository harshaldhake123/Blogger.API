using Blogger.Infrastructure.Database.Data;
using Blogger.UseCases.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Infrastructure.Database.UnitTests;

public class SqlUserRepositoryTests
{
    private readonly BloggerDbContext _bloggerDbContext;
    private readonly SqlUserRepository _sqlUserRepository;

    public SqlUserRepositoryTests()
    {
        DbContextOptions<BloggerDbContext> options = new DbContextOptionsBuilder<BloggerDbContext>()
            .UseInMemoryDatabase("SqlUserRepositoryTests" + DateTime.UtcNow.ToFileTime()).Options;
        _bloggerDbContext = new BloggerDbContext(options);
        _sqlUserRepository = new SqlUserRepository(_bloggerDbContext);
    }

    [Fact]
    public async Task CreateUser_should_save_user_to_database()
    {
        User user = new()
        {
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "abc@email.com",
            Password = "abcd",
        };

        await _sqlUserRepository.CreateUser(user);

        Assert.Equal(user, _bloggerDbContext.User.First(u => u.EmailAddress == user.EmailAddress));
    }

    [Fact]
    public async Task When_EmailAddress_already_exists_then_EmailAddressAlreadyExists_should_return_true()
    {
        var emailAddress = "abc@email.com";
        User user = new()
        {
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = emailAddress,
            Password = "abcd",
        };
        _bloggerDbContext.User.Add(user);
        _bloggerDbContext.SaveChanges();
        bool actual = await _sqlUserRepository.EmailAddressAlreadyExists(emailAddress);

        Assert.True(actual);
    }

    [Fact]
    public async Task When_EmailAddress_does_not_exist_then_EmailAddressAlreadyExists_should_return_false()
    {
        string emailAddress = "abc@email.com";

        bool actual = await _sqlUserRepository.EmailAddressAlreadyExists(emailAddress);

        Assert.False(actual);
    }
}