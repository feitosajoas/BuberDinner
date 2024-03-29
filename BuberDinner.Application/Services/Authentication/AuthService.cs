using System;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using OneOf;

namespace BuberDinner.Application.Services.Authentication;

public class AuthService : IAuthService
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public AuthService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
  {
    _jwtTokenGenerator = jwtTokenGenerator;
    _userRepository = userRepository;
  }

  public OneOf<AuthResult, DuplicateEmailError> Register(string firstName, string lastName, string email, string password)
  {
    // 1. validate the user doesn't exist
    if (_userRepository.GetUserByEmail(email) is not null)
    {
      return new DuplicateEmailError();
    }

    // 2. Create user (generate unique ID) & Persist to DB
    var user = new User
    {
      FirstName = firstName,
      LastName = lastName,
      Email = email,
      Password = password
    };

    _userRepository.Add(user);

    // 3. Create JWT token
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthResult(
      user,
      token);
  }

  public AuthResult Login(string email, string password)
  {
    // 1. Validate the user exists
    if (_userRepository.GetUserByEmail(email) is not User user)
    {
      throw new Exception("User with given email does not exists.");
    }

    // 2. Validate the password is correct
    if (user.Password != password)
    {
      throw new Exception("Invalid password.");
    }

    // 3 Create JWT token
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthResult(
      user,
      token);
  }
}