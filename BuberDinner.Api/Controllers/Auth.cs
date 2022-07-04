using System.Net.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Contracts.Authentication;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class Auth : ControllerBase 
{
	[HttpPost("register")]
	public IActionResult Register(RegisterRequest request)
	{
		return Ok(request);
	}

	[HttpPost("login")]
	public IActionResult Login(LoginRequest request)
	{
		return Ok(request);
	}
}