using System.Net.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Application.Common.Errors;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
  private readonly IAuthService _authService;

  public AuthController(IAuthService authService)
  {
    _authService = authService;
  }

  [HttpPost("register")]
  public IActionResult Register(RegisterRequest request)
  {
    OneOf<AuthResult, DuplicateEmailError> registerResult = _authService.Register(
      request.FirstName,
      request.LastName,
      request.Email,
      request.Password);

    if (registerResult.IsT0<AuthResult>())
    {
      var authResult = registerResult.AsT0;
      var response = new AuthenticationResponse(
      authResult.User.Id,
      authResult.User.FirstName,
      authResult.User.LastName,
      authResult.User.Email,
      authResult.Token
    );
    }
    else
    {
      return BadRequest(registerResult.Value);
    }
    
    return Ok(response);
  }

  [HttpPost("login")]
  public IActionResult Login(LoginRequest request)
  {
    var authResult = _authService.Login(
      request.Email,
      request.Password);

    var response = new AuthenticationResponse(
      authResult.User.Id,
      authResult.User.FirstName,
      authResult.User.LastName,
      authResult.User.Email,
      authResult.Token
    );
    return Ok(response);
  }
}