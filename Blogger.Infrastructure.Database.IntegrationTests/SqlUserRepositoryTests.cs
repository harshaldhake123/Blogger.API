using Blogger.Domain.Core.Entities;
using Blogger.Infrastructure.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Infrastructure.Database.IntegrationTests;

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
    public async Task CreateUserShouldSaveUserToDatabase()
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
    public async Task WhenEmailAddressAlreadyExistsThenEmailAddressAlreadyExistsShouldReturnTrue()
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
    public async Task WhenEmailAddressDoesNotExistThenEmailAddressAlreadyExistsShouldReturnFalse()
    {
        string emailAddress = "abc@email.com";

        bool actual = await _sqlUserRepository.EmailAddressAlreadyExists(emailAddress);

        Assert.False(actual);
    }

    [Fact]
    public async Task GetUserShouldReturnUser()
    {
        User user = new()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            EmailAddress = "abc@email.com",
            Password = "abcd",
        };
        _bloggerDbContext.User.Add(user);
        _bloggerDbContext.SaveChanges();

        var actual = await _sqlUserRepository.GetUser(user.EmailAddress);

        Assert.Equal(user, actual);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenUserDoesntExist()
    {
        User user = new()
        {
            EmailAddress = "abc@email.com",
            Password = "abcd",
        };
        var actual = await _sqlUserRepository.GetUser(user.EmailAddress);

        Assert.Null(actual);
    }

    [Fact]
    public async Task UpdateUserUpdatesUserInDb()
    {
        User user = new()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            EmailAddress = "abc@email.com",
            Password = "abcd",
        };
        _bloggerDbContext.User.Add(user);
        _bloggerDbContext.SaveChanges();

        user.Password = "updated password";
        await _sqlUserRepository.UpdateUser(user);

        User updatedUser = _bloggerDbContext.User.First(u => u.EmailAddress == user.EmailAddress);
        Assert.Equal(user.Password, updatedUser.Password);
    }
}