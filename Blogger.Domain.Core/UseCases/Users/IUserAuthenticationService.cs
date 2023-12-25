﻿using Blogger.Domain.Core.Entities;

namespace Blogger.Domain.Core.UseCases.Users;

public interface IUserAuthenticationService
{
    public string HashPassword(User user);

    public Task<bool> VerifyPassword(User user, string storedHashedPassword);
}